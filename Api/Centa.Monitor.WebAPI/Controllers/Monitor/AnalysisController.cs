#region using
using AutoMapper;
using Centa.Monitor.WebApi.Filter.Action;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Centa.Monitor.ViewModel.Response;
using Centa.Monitor.WebApi.ModelBinder;
using Centa.Monitor.ViewModel.Request.System.Project;
using Centa.Monitor.ViewModel.Response.System.Project;
using Centa.Monitor.ApplicationService.Interface.Monitor;
using Centa.Monitor.Dto.Monitor.Behavior;
using Centa.Monitor.Dto.Monitor.CustomerPV;
using Centa.Monitor.Dto.Monitor.ExtendBehavior;
using Centa.Monitor.Dto.Monitor.HttpLog;
using Centa.Monitor.Dto.Monitor.JavascriptError;
using Centa.Monitor.Dto.Monitor.LoadPage;
using Centa.Monitor.Dto.Monitor.ResourceLoad;
using Centa.Monitor.Dto.Monitor.ScreenShot;
#endregion

namespace Centa.Monitor.WebAPI.Controllers.Monitor
{
    #region analysis
    /// <summary>
    /// 监控分析
    /// </summary>
    [ApiVersion("1.1")]
    [Route("api/monitor/v{version:apiVersion}/analysis/")]
    [TokenValidate]
    public class AnalysisController : Controller
    {
        #region private
        private readonly IBehaviorService _behaviorService;

        private readonly ICustomerPVService _customerPVService;

        private readonly IExtendBehaviorService _extendBehaviorService;

        private readonly IHttpLogService _httpLogService;

        private readonly IJavascriptErrorService _javascriptErrorService;

        private readonly ILoadPageService _loadPageService;

        private readonly IResourceLoadService _resourceLoadService;

        private readonly IScreenShotService _screenShotService;
        #endregion

        #region ioc
        public AnalysisController(IBehaviorService behaviorService
            , ICustomerPVService customerPVService
            , IExtendBehaviorService extendBehaviorService
            , IHttpLogService httpLogService
            , IJavascriptErrorService javascriptErrorService
            , ILoadPageService loadPageService
            , IResourceLoadService resourceLoadService
            , IScreenShotService screenShotService)
        {
            _behaviorService = behaviorService;
            _customerPVService = customerPVService;
            _extendBehaviorService = extendBehaviorService;
            _httpLogService = httpLogService;
            _javascriptErrorService = javascriptErrorService;
            _loadPageService = loadPageService;
            _resourceLoadService = resourceLoadService;
            _screenShotService = screenShotService;
        }
        #endregion

        #region user behavior
        /// <summary>
        /// 用户行为
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("behaviors")]
        [SwaggerOperation(Tags = new[] { "分析" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetBehaviorList(BahaviorRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<BehaviorSearchDto>(query);
            var list = _behaviorService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region PV
        /// <summary>
        /// PV
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("customer-pvs")]
        [SwaggerOperation(Tags = new[] { "分析" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetCustomerPVList(CustomerPVRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<CustomerPVSearchDto>(query);
            var list = _customerPVService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region extend behavior
        /// <summary>
        /// 自定义行为
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("extend-behaviors")]
        [SwaggerOperation(Tags = new[] { "分析" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetExtendBehaviorList(ExtendBehaviorRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<ExtendBehaviorSearchDto>(query);
            var list = _extendBehaviorService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region http log
        /// <summary>
        /// HTTP请求
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("http-logs")]
        [SwaggerOperation(Tags = new[] { "分析" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetHttpLogList(HttpLogRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<HttpLogSearchDto>(query);
            var list = _httpLogService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region js-errors
        /// <summary>
        /// js错误
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("js-errors")]
        [SwaggerOperation(Tags = new[] { "分析" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetJavascriptErrorList(JavascriptErrorRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<JavascriptErrorSearchDto>(query);
            var list = _javascriptErrorService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region load page
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("load-pages")]
        [SwaggerOperation(Tags = new[] { "分析" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetLoadPageList(LoadPageRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<LoadPageSearchDto>(query);
            var list = _loadPageService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region unload resource
        /// <summary>
        /// 错误的资源文件
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("load-resources")]
        [SwaggerOperation(Tags = new[] { "分析" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetResourceLoadList(ResourceLoadRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<ResourceLoadSearchDto>(query);
            var list = _resourceLoadService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion

        #region screen shot
        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpGet("screen-shots")]
        [SwaggerOperation(Tags = new[] { "分析" })]
        [SwaggerResponse(200, Type = typeof(ProjectListResponseViewModel), Description = "成功")]
        public IActionResult GetScreenShotList(ScreenShotRequestViewModel query, TokenModel tokenModel)
        {
            var searchModel = Mapper.Map<ScreenShotSearchDto>(query);
            var list = _screenShotService.GetList(searchModel);
            return Ok(new PageResponseViewModel() { Data = list.Items, Page = new PageViewModel { PageSize = query.PageSize, Total = list.TotalNum, PageIndex = query.PageIndex } });
        }
        #endregion
    }
    #endregion
}
