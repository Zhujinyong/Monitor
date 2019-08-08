using Centa.Monitor.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Centa.Monitor.Common.Exceptions;
using Centa.Monitor.Infrastructure.Enum;
using Centa.Monitor.Common.Log4net;

namespace Centa.Monitor.WebApi.Filter.Exception
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public CustomExceptionFilterAttribute()
        {
        }

        public override void OnException(ExceptionContext context)
        {
            #region 自定义异常
            if (context.Exception.GetType()== typeof(CustomException))
            {
                context.Result = new JsonResult(new ResponseViewModel(StatusCodeEnum.InternalError, context.Exception.ToString()));
            }
            #endregion

            #region 捕获程序异常，友好提示
            else
            {
                if (context.Exception != null)
                {
                    Log.WriteLogError(this, context.Exception.ToString());
                }
                context.Result = new JsonResult(new ResponseViewModel(StatusCodeEnum.InternalError, "服务器忙，请稍后再试"));
            }
            #endregion
        }
    }
}
