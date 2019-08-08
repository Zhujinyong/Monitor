using Centa.Monitor.Infrastructure.General.Page;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Centa.Monitor.Infrastructure.Interfaces
{
    public interface IBaseUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }

        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<dynamic> Query(string sql);

        int Excute(string sql, object parameters = null, CommandType? commandType = CommandType.Text);

        IEnumerable<dynamic> Query(string sql, object parameters = null, CommandType? commandType = CommandType.Text);

        IEnumerable<TEntity> Query<TEntity>(string sql, object parameters = null, CommandType? commandType = CommandType.Text);

        PageDataView<TEntity> GetPageData<TEntity>(string sqlDistinct, Dictionary<string, bool> sort = null, int? pageIndex = null, int? pageSize = null, DynamicParameters parameters = null, CommandType? commandType = CommandType.Text) where TEntity : class;
    }
}
