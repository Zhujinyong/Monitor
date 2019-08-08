using Centa.Monitor.Infrastructure.General.Page;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Infrastructure.Interfaces
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="action"></param>
        void RunTransaction(Action action);

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
    }
}