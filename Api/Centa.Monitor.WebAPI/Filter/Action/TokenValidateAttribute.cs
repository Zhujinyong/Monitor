using Centa.Monitor.Infrastructure.Enum;
using Centa.Monitor.ViewModel.Response;
using Centa.Monitor.WebApi.ModelBinder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Centa.Monitor.WebApi.Filter.Action
{
    /// <summary>
    /// token验证，加上模型绑定，不需要在每个Action里写
    /// </summary>
    public class TokenValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!(context.HttpContext.Request.Headers.ContainsKey("token")))
            {
                context.Result = new OkObjectResult(new ResponseViewModel(StatusCodeEnum.ParamInvalid, "参数错误"));
                return;
            }

            if (!context.ActionArguments.Any(p => p.Value.GetType() == typeof(TokenModel)))
            {
                context.Result = new OkObjectResult(new ResponseViewModel(StatusCodeEnum.ParamInvalid, "缺少TokenModel参数"));
                return;

                
            }
            var param = context.ActionArguments.FirstOrDefault(p => p.Value.GetType() == typeof(TokenModel)).Value;
            var result = param as TokenModel;
            if (result == null||string.IsNullOrEmpty(result.DomainAccount))
            {
                context.Result = new OkObjectResult(new ResponseViewModel(StatusCodeEnum.TokenInvalid, "token无效"));
                return;
            }
            if (result.ValidateTo < System.DateTime.Now)
            {
                context.Result = new OkObjectResult(new ResponseViewModel(StatusCodeEnum.TokenExpired, "token过期,请重新登录"));
                return;
            }
        }
    }
}
