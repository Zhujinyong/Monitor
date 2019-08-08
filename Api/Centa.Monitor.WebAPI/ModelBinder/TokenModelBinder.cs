using AutoMapper;
using Centa.Monitor.ApplicationService.Interface;
using Centa.Monitor.ApplicationService.Interface.System;
using Centa.Monitor.WebApi.Token;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using static Centa.Monitor.Common.Extensions;

namespace Centa.Monitor.WebApi.ModelBinder
{
    /// <summary>
    /// 模型绑定，解析query的token
    /// </summary>
    public class TokenModelBinder : IModelBinder
    {
        private readonly ITokenVerify _tokenService;

        private readonly IUserService _userService;

        public TokenModelBinder(ITokenVerify tokenService, IUserService accountService)
        {
            _tokenService = tokenService;
            _userService = accountService;
        }

        /// <summary>
        /// 请求header里传递参数token
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //参数必须包含token
            if (!(bindingContext.ActionContext.HttpContext.Request.Headers.ContainsKey("token")))
                return Task.CompletedTask;
            var token = bindingContext.ActionContext.HttpContext.Request.Headers["token"];
            //  解析token
            TokenModel result;

            try
            {
                result = _tokenService.ReadToken(token);
            }
            catch (Exception e)
            {
                return Task.CompletedTask;
            }
            var validateTo = result.ValidateTo;

            #region 如果后台禁用用户，需要更新模型绑定相关字段
            // TODO
            var account = _userService.GetAccount(result.DomainAccount);
            if (account == null)
            {
                return Task.CompletedTask;
            }
            result = Mapper.Map<TokenModel>(account);
            result.ValidateTo = validateTo;

            #endregion

            result.IPAddress = bindingContext.ActionContext.HttpContext.GetClientUserIp();
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
