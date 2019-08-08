using ArxOne.MrAdvice.Advice;
using Centa.CCME.Common.LogBImplements.Aspects.ForAny;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Centa.CCME.WebAPI
{
    /// <summary>
    /// 具体异常AOP特性类
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class CentaExceptionLog : ExceptionAspect
    {
        /// <summary>
        /// 发生异常事件
        /// </summary>
        /// <param name="context">方法元数据</param>
        /// <param name="exception">异常实例</param>
        protected override void OnException(MethodAdviceContext context, Exception exception)
        {
            base.OnException(context, exception);
            //发生异常返回统一的结果
            context.ReturnValue = new StatusCodeResult(404);
        }
    }
}
