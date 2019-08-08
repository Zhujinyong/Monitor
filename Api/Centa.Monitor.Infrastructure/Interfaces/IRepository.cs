using Centa.Monitor.Infrastructure.General.Page;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Infrastructure.Interfaces
{
    /// <summary>
    /// 数据库CRUD等操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria">查询设置</param>
        /// <param name="param"></param>
        /// <returns></returns>
        PageDataView<TEntity> GetPageData<TEntity>(PageCriteria criteria, object param = null, bool readOnly = false) where TEntity : class;

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        long Add(T entity);

        /// <summary>  
        /// 批量插入功能  
        /// </summary>  
        void InsertBatch<T>(List<T> data) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        void Update(T entity);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        void Remove(T entity);

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        T GetByKeyId(Guid KeyId, bool readOnly = false);

        /// <summary>
        /// 获取多个实体对象
        /// </summary>
        /// <param name="KeyIds">主键KeyIds</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        IEnumerable<T> GetByKeyIds(List<Guid> KeyIds, bool readOnly = false);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(bool readOnly = false);

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> GetBy(string where = null, object param = null, object order = null, bool readOnly = false);

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<dynamic> Query(string sql, object param = null, CommandType? commandType = CommandType.Text, bool readOnly = false);

        /// <summary>
        ///  查询数据列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Query<TEntity>(string sql, object param = null, CommandType? commandType = CommandType.Text, bool readOnly = false);

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        int Execute(string sql, object parameters = null, CommandType? commandType = CommandType.Text, bool readOnly = false);

        /// <summary>
        /// SQL语句分页查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlDistinct">sql语句</param>
        /// <param name="sort">排序字段集合</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页条数</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        PageDataView<TEntity> GetPageData<TEntity>(string sqlDistinct, Dictionary<string, bool> sort = null, int? pageIndex = null, int? pageSize = null, DynamicParameters parameters = null, bool readOnly = false) where TEntity : class;
    }
}