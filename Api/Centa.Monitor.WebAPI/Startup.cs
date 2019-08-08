#region using
using Centa.Monitor.Common.DependencyInjection;
using Centa.Monitor.Dto.System.User;
using Centa.Monitor.Infrastructure;
using Centa.Monitor.Infrastructure.Model.System;
using Centa.Monitor.WebApi.Filter.Exception;
using Centa.Monitor.WebApi.ModelBinder;
using Centa.Monitor.WebApi.Swagger;
using Centa.Monitor.WebApi.Token;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Centa.Monitor.ApplicationService.Interface.System;
using Centa.Monitor.ViewModel.Request.System.User;
using Centa.Monitor.ViewModel.Request.System.Project;
using Centa.Monitor.Dto.System.Project;
using Centa.Monitor.Infrastructure.Model.System.Project;
#endregion

namespace Centa.Monitor.WebApi
{
    #region Startup
    public class Startup
    {
        #region private
        private IConfiguration Configuration { get; }
        #endregion

        #region constructor
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        #endregion

        #region ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //注入AppSetting，其他类库可以引用，如DapperContext构造函数用到了
            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));
            //缓存
            services.AddMemoryCache();

            #region 跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            #endregion

            #region  依赖注入相关自定义接口
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITokenVerify, TokenVerify>();
            services.RegisterRepository("Centa.Monitor.Infrastructure", string.Empty);
            services.RegisterService("Centa.Monitor.ApplicationService", string.Empty);
            #endregion

            services.AddRouting(options => options.LowercaseUrls = true);
            #region swagger
            services.AddSwaggerGen(
                options =>
                {
                    var provider = services.BuildServiceProvider()
                        .GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {

                        options.SwaggerDoc(
                            description.GroupName,
                            new Info()
                            {
                                Title = $"{Configuration.GetSection("AppSetting:ApiTitle").Value}",
                                Version = description.ApiVersion.ToString()
                            });
                    }
                    options.DocInclusionPredicate((docName, apiDesc) =>
                    {
                        var versions = apiDesc.ControllerAttributes()
                            .OfType<ApiVersionAttribute>()
                            .SelectMany(attr => attr.Versions);
                        return versions.Any(v => $"v{v.ToString()}" == docName);
                    });
                    //options.DescribeAllEnumsAsStrings();
                    //模型绑定TokenModel参数设置
                    options.OperationFilter<ParameterFilter>();

                    options.DocumentFilter<VersionInPathFilter>();
                    var basePath = System.IO.Directory.GetCurrentDirectory();
                    //Console.WriteLine(basePath);
                    options.IncludeXmlComments(System.IO.Path.Combine(basePath, "wwwroot", "Centa.Monitor.WebAPI.xml"));
                    //Dto作为一个单独项目，需要也包含就来，否则生成的xml里没有相应的注释
                    options.IncludeXmlComments(System.IO.Path.Combine(basePath, "wwwroot", "Centa.Monitor.ViewModel.xml"));
                });
            #endregion

            #region apiVerison
            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
            });
            #endregion

            #region mvc
            services.AddMvcCore().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.ModelBinderProviders.Insert(0, new TokenModelBinderProvider(services.BuildServiceProvider().GetService<ITokenVerify>(), services.BuildServiceProvider().GetService<IUserService>()));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            #endregion

            #region Mini
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
            });
            #endregion
        }
        #endregion

        #region Configure
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            #region 解决linux Nginx 代理不能获取IP问题
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            #endregion

            #region AutoMapper
            AutoMapper.Mapper.Initialize(mapper =>
            {
                #region User
                mapper.CreateMap<UserRequestViewModel, UserSearchDto>();
                mapper.CreateMap<UpdateUserRequestViewModel, UpdateUserDto>();
                mapper.CreateMap<UpdateUserDto, UserModel>();
                mapper.CreateMap<DeleteUserRequestViewModel, DeleteUserDto>();
                mapper.CreateMap<AddUserRequestViewModel, UserAddDto>();
                mapper.CreateMap<UserAddDto, UserModel>();
                mapper.CreateMap<LoginRequestViewModel, LoginDto>();
                mapper.CreateMap<UserModel, TokenModel>();
                #endregion

                #region Project
                mapper.CreateMap<AddProjectRequestViewModel, AddProjectDto>();
                mapper.CreateMap <ProjectRequestViewModel, SearchProjectDto> ();
                mapper.CreateMap <UpdateProjectRequestViewModel, UpdateProjectDto> ();
                mapper.CreateMap <DeleteProjectRequestViewModel, DeleteProjectDto> ();
                mapper.CreateMap <AddProjectDto, ProjectModel> ();
                mapper.CreateMap <UpdateProjectDto, ProjectModel> ();
                #endregion
            });
            #endregion

            #region swagger configure
            app.UseSwagger(c => { });
            app.UseSwaggerUI(
                   options =>
                   {
                       options.DefaultModelRendering(ModelRendering.Model);
                       options.DefaultModelsExpandDepth(-1);
                       options.DisplayRequestDuration();
                       options.DocumentTitle = $"{Configuration.GetSection("AppSetting:ApiTitle").Value}";
                       foreach (var description in provider.ApiVersionDescriptions)
                       {
                           options.SwaggerEndpoint(
                               $"/swagger/{description.GroupName}/swagger.json",
                               description.GroupName.ToUpperInvariant());
                       }
                   });
            #endregion

            app.UseCors("AllowAllOrigins");
            app.UseMvc();
        }
        #endregion
    }
    #endregion
}
