﻿using ArxOne.MrAdvice.Advice;
using Centa.CCME.Common.LogBImplements.Mediators;
using Centa.CCME.Common.LogBImplements.Models.Entities;
using Centa.CCME.Common.LogBImplements.Toolkits;
using System;
using System.Transactions;

namespace Centa.CCME.Common.LogBImplements.Aspects.ForMethod
{
    /// <summary>
    /// 程序日志AOP特性类
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true)]
    public class RunningLogAspect : Attribute, IMethodAdvice
    {
        /// <summary>
        /// 程序日志字段
        /// </summary>
        protected readonly RunningLog _runningLog;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RunningLogAspect()
        {
            this._runningLog = new RunningLog();
        }

        /// <summary>
        /// 拦截方法
        /// </summary>
        /// <param name="context">方法元数据</param>
        public  void Advise(MethodAdviceContext context)
        {
            this._runningLog.BuildRuningInfo(context);
            this._runningLog.BuildBasicInfo(context);
            this._runningLog.BuildMethodArgsInfo(context);

            context.Proceed();

            this._runningLog.BuildReturnValueInfo(context);

            //无需事务
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled))
            {
                //持久化
                  LogMediator.Write(this._runningLog);

                scope.Complete();
            }
        }
    }
}