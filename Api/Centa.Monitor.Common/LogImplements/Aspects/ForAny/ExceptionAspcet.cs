﻿using ArxOne.MrAdvice.Advice;
using Centa.CCME.Common.LogBImplements.Attributes;
using Centa.CCME.Common.LogBImplements.Mediators;
using Centa.CCME.Common.LogBImplements.Models.Entities;
using Centa.CCME.Common.LogBImplements.Models.ValueObjects;
using Centa.CCME.Common.LogBImplements.Toolkits;
using System;
using System.Transactions;

namespace Centa.CCME.Common.LogBImplements.Aspects.ForAny
{
    /// <summary>
    /// 异常AOP特性基类
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public abstract class ExceptionAspect : Attribute, IMethodAdvice
    {
        /// <summary>
        /// 异常日志字段
        /// </summary>
        protected readonly ExceptionLog _exceptionLog;

        /// <summary>
        /// 异常消息
        /// </summary>
        protected ExceptionMessage _exceptionMessage;

        /// <summary>
        /// 构造器
        /// </summary>
        protected ExceptionAspect()
        {
            this._exceptionLog = new ExceptionLog();
        }

        /// <summary>
        /// 拦截方法
        /// </summary>
        /// <param name="context">方法元数据</param>
        public   void Advise(MethodAdviceContext context)
        {
            try
            {
                 context.Proceed();
            }
            catch (Exception exception)
            {
                this.OnException(context, exception);
            }
        }

        /// <summary>
        /// 发生异常事件
        /// </summary>
        /// <param name="context">方法元数据</param>
        /// <param name="exception">异常实例</param>
        protected virtual void OnException(MethodAdviceContext context, Exception exception)
        {
            if (!context.TargetMethod.IsDefined(typeof(SkipExceptionAttribute), true))
            {
                //初始化异常日志
                this._exceptionLog.BuildBasicInfo(context);
                this._exceptionLog.BuildMethodArgsInfo(context);
                this._exceptionLog.BuildExceptionInfo(exception);

                //无需事务
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled))
                {
                    //插入数据库
                    Guid newId = LogMediator.Write(this._exceptionLog);

                    scope.Complete();

                    //初始化异常消息
                    this._exceptionMessage = new ExceptionMessage(exception.Message, newId);
                }
            }
        }
    }
}