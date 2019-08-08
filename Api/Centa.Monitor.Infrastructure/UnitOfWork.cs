using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Centa.Monitor.Infrastructure.Interfaces;
using System.Text;
using Centa.Monitor.Infrastructure.General.Page;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace Centa.Monitor.Infrastructure
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public IDapperContext Context { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        /// <summary>
        /// DB执行超时时间
        /// </summary>
        public int DbCommandTimeOut
        {
            get
            {
                int timeout = ((DapperContext)Context).CommandTimeOut;
                if (timeout > 0)
                {
                    return timeout;
                }
                return 3000;
            }
        }

        private int TransactionCount = 0;

        public IDbConnection Connection
        {
            get
            {
                return this.Context.Connection;
            }
        }

        public UnitOfWork(IDapperContext context)
        {
            Context = context;
        }

        public void BeginTransaction()
        {
            if (TransactionCount == 0 && Transaction == null)
            {
                Transaction = Context.Connection.BeginTransaction();
            }
            TransactionCount++;
        }

        public void Commit()
        {
            if (Transaction != null)
            {
                if (TransactionCount == 1)
                {
                    Transaction.Commit();
                    this.Dispose();
                    //Transaction.Dispose();
                }
                TransactionCount--;
            }
            else
            {
                throw new NullReferenceException("Tryed commit not opened transaction");
            }
        }

        public void Rollback()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
            }
            Transaction = null;
        }

        private bool HasActiveTransaction
        {
            get
            {
                return Transaction != null;
            }
        }

        public void RunTransaction(Action action)
        {
            BeginTransaction();
            try
            {
                action();
                Commit();
            }
            catch (Exception ex)
            {
                if (HasActiveTransaction)
                {
                    Rollback();
                }
                throw ex;
            }
        }

        public int Excute(string sql, object parameters = null, CommandType? commandType = CommandType.Text)
        {
            return Context.Connection.Execute(sql, parameters, transaction: Transaction, commandTimeout: DbCommandTimeOut, commandType: commandType);
        }

        public IEnumerable<dynamic> Query(string sql)
        {
            return Context.Connection.Query<dynamic>(sql);
        }

        public IEnumerable<T> Query<T>(string sql, object parameters = null, CommandType? commandType = CommandType.Text)
        {
            return Context.Connection.Query<T>(sql.ToString(), parameters, transaction: Transaction, commandTimeout: DbCommandTimeOut, commandType: commandType);
        }

        public IEnumerable<dynamic> Query(string sql, object parameters = null, CommandType? commandType = CommandType.Text)
        {
            return Context.Connection.Query(sql, parameters, transaction: Transaction, commandTimeout: DbCommandTimeOut, commandType: commandType);
        }

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
        public PageDataView<TEntity> GetPageData<TEntity>(string sqlDistinct, Dictionary<string, bool> sort, int? pageIndex = null, int? pageSize = null, DynamicParameters parameters = null, CommandType? commandType = CommandType.Text) where TEntity : class
        {
            try
            {
                if (string.IsNullOrEmpty(sqlDistinct))
                {
                    throw new Exception("查询语句为空");
                }
                StringBuilder select = new StringBuilder();
                int skipNum = 0;

                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    select.AppendFormat("SELECT TOP ({0}) * ", pageSize.Value);
                    skipNum = pageSize.Value * (pageIndex.Value - 1);
                }
                else
                {
                    select.Append("SELECT * ");
                }

                StringBuilder order = new StringBuilder();

                foreach (string key in sort.Keys)
                {
                    if (key.Contains(","))
                    {
                        string[] arr = key.Split(',');
                        foreach (string a in arr)
                        {
                            if (!string.IsNullOrEmpty(a))
                            {
                                order.AppendFormat("{0} {1},", a, sort[key] == true ? "ASC" : "DESC");
                            }
                        }
                    }
                    else
                    {
                        order.AppendFormat("{0} {1},", key, sort[key] == true ? "ASC" : "DESC");
                    }
                }
                order.Remove(order.Length - 1, 1);

                //返回当前页数据
                StringBuilder sql = new StringBuilder();
                sql.Append(select);
                sql.AppendFormat("FROM (SELECT *,row_number() OVER (ORDER BY {0}) AS row_number ", order);
                sql.AppendFormat("FROM ({0}) AS t1) AS t2 ", sqlDistinct);
                sql.AppendFormat("WHERE row_number>{0} ", skipNum);
                sql.AppendFormat("ORDER BY {0} ", order);

                var pageData = new PageDataView<TEntity>();
                //查询数据
                pageData.Items = Context.Connection.Query<TEntity>(sql.ToString(), parameters, commandTimeout: DbCommandTimeOut, commandType: CommandType.Text).ToList();
                if (pageData.Items.Count > 0 && pageIndex.HasValue && pageSize.HasValue)
                {
                    pageData.TotalNum = this.GetPageCount(sqlDistinct, parameters);
                    pageData.CurrentPage = pageIndex.Value;
                }
                return pageData;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 分页查询总数
        /// </summary>
        /// <param name="sqlDistinct"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private int GetPageCount(string sqlDistinct, DynamicParameters parameters = null)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT COUNT(1) FROM (select * from (");
            sqlQuery.AppendFormat(sqlDistinct.ToString());
            sqlQuery.AppendFormat(") tp) AS Distinct1");
            return Context.Connection.Query<int>(sqlQuery.ToString(), parameters).FirstOrDefault();
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
        }

        ~UnitOfWork()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
            }
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}