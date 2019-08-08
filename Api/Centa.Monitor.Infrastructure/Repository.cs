using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Centa.Monitor.Common;
using System.Data.SqlClient;

namespace Centa.Monitor.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public int DbCommandTimeout
        {
            get
            {
                return ((UnitOfWork)_unitOfWork).DbCommandTimeOut;
            }
        }

        private IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IBaseUnitOfWork GetUnitOfWork(bool readOnly = false)
        {
            return _unitOfWork;
        }

        private IDbTransaction GetTransaction()
        {
            return ((UnitOfWork)_unitOfWork).Transaction;
        }

        public PageDataView<TEntity> GetPageData<TEntity>(PageCriteria criteria, object param = null, bool readOnly = false) where TEntity : class
        {
            var p = new DynamicParameters();
            string proName = "ProcGetPageData";
            p.Add("TableName", criteria.TableName);
            p.Add("PrimaryKey", criteria.PrimaryKey);
            p.Add("Fields", criteria.Fields);
            p.Add("Condition", criteria.Condition);
            p.Add("CurrentPage", criteria.CurrentPage);
            p.Add("PageSize", criteria.PageSize);
            p.Add("Sort", criteria.Sort);
            p.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var pageData = new PageDataView<TEntity>();
            int excuteTimes = 0;
            QUERY:
            try
            {
                //pageData.Items = Connection.Query<TEntity>(proName, p, commandType: CommandType.StoredProcedure).ToList();
                pageData.Items = GetUnitOfWork(readOnly).Connection.Query<TEntity>(proName, p, commandType: CommandType.StoredProcedure).ToList();
                pageData.TotalNum = p.Get<int>("RecordCount");
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.TotalNum * 1.0 / criteria.PageSize));
                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;
                return pageData;
            }
            catch (Exception e)
            {
                excuteTimes++;
                if (e.Message.Contains("找不到存储过程 'ProcGetPageData'") && excuteTimes <= 3)
                {
                    FileStream fileStream = new FileStream("ProcGetPageData.sql", FileMode.Open);
                    StreamReader sr = new StreamReader(fileStream);
                    string sql = sr.ReadToEnd();
                    if (fileStream != null)
                        fileStream.Close();
                    if (sr != null)
                        sr.Close();
                    if (!string.IsNullOrEmpty(sql))
                    {
                        //Connection.Execute(sql);
                        this.GetUnitOfWork(false).Connection.Execute(sql);
                        goto QUERY;
                    }
                    throw;
                }
                else if ((e.Message.Contains("远程主机强迫关闭了一个现有的连接") || e.Message.Contains("指定的网络名不再可用")) && excuteTimes <= 3)
                {
                    System.Threading.Thread.Sleep(excuteTimes * 1000);
                    goto QUERY;
                }
                else
                {
                    throw;
                }
            }
        }

        public long Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Add to DB null entity");
            }
            var res = this.GetUnitOfWork(false).Connection.Insert(entity, GetTransaction(), commandTimeout: DbCommandTimeout);
            return res;
        }

        /// <summary>  
        /// 批量插入功能  
        /// </summary>  
        public void InsertBatch<T>(List<T> data) where T : class
        {
            if (data == null || data.Count == 0)
            {
                return;
            }
            Type type = typeof(T);
            var attribute = type.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();
            if (attribute == null)
            {
                throw new Exception("table name is empty");
            }
            string tableName = ((TableAttribute)attribute).Name;
            var tran = GetTransaction() == null ? null : (SqlTransaction)GetTransaction();
            DataTable dt = data.AsToDataTable();
            using (var bulkCopy = new SqlBulkCopy((SqlConnection)_unitOfWork.Connection, SqlBulkCopyOptions.Default, tran))
            {
                var props = type.GetProperties();
                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                }

                bulkCopy.BatchSize = data.Count;
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.WriteToServer(dt);
            }
        }


        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Update in DB null entity");
            }
            this.GetUnitOfWork(false).Connection.Update(entity, GetTransaction(), DbCommandTimeout);
        }

        public virtual void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Remove in DB null entity");
            }
            this.GetUnitOfWork(false).Connection.Delete(entity, transaction: GetTransaction(), commandTimeout: DbCommandTimeout);
        }

        public virtual T GetByKeyId(Guid KeyId, bool readOnly = false)
        {
            if (Guid.Empty.Equals(KeyId))
            {
                throw new ArgumentNullException("id");
            }
            return this.GetUnitOfWork(readOnly).Connection.Get<T>(KeyId, transaction: GetTransaction(), commandTimeout: DbCommandTimeout);
        }

        /// <summary>
        /// 获取多个实体对象
        /// </summary>
        /// <param name="KeyIds">主键KeyIds</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetByKeyIds(List<Guid> KeyIds, bool readOnly = false)
        {
            if (KeyIds == null || KeyIds.Count <= 0)
            {
                throw new ArgumentNullException("no keyids");
            }
            //获取表名
            Type t = typeof(T);
            var attribute = t.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();
            if (attribute == null)
            {
                throw new Exception("table name is empty");
            }
            string tableName = ((TableAttribute)attribute).Name;
            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception("table name is empty");
            }
            string select = $"SELECT * FROM {tableName} t INNER JOIN @KeyIds list ON t.KeyId  = list.Item ";
            return this.GetUnitOfWork(readOnly).Connection.Query<T>(select,
                new { KeyIds = Extensions.MakeListParam(KeyIds) }, GetTransaction(), commandTimeout: DbCommandTimeout);
        }

        public virtual IEnumerable<T> GetAll(bool readOnly = false)
        {
            return this.GetUnitOfWork(readOnly).Connection.GetAll<T>(transaction: GetTransaction(), commandTimeout: DbCommandTimeout);
        }

        public virtual IEnumerable<T> GetBy(string where = null, object param = null, object order = null, bool readOnly = false)
        {
            return this.GetUnitOfWork(readOnly).Connection.Query<T>(where, param, transaction: GetTransaction(), commandTimeout: DbCommandTimeout);
        }

        public IEnumerable<dynamic> Query(string sql, object param = null, CommandType? commandType = CommandType.Text, bool readOnly = false)
        {
            return this.GetUnitOfWork(readOnly).Connection.Query<dynamic>(sql, param, GetTransaction(), commandTimeout: DbCommandTimeout, commandType: commandType);
        }

        public IEnumerable<TEntity> Query<TEntity>(string sql, object param = null, CommandType? commandType = CommandType.Text, bool readOnly = false)
        {
            return this.GetUnitOfWork(readOnly).Connection.Query<TEntity>(sql, param, GetTransaction(), commandTimeout: DbCommandTimeout, commandType: commandType);
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public int Execute(string sql, object parameters = null, CommandType? commandType = CommandType.Text, bool readOnly = false)
        {
            return this.GetUnitOfWork(readOnly).Connection.Execute(sql, parameters, GetTransaction(), DbCommandTimeout, commandType);
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
        public PageDataView<TEntity> GetPageData<TEntity>(string sqlDistinct, Dictionary<string, bool> sort, int? pageIndex = null, int? pageSize = null, DynamicParameters parameters = null, bool readOnly = false) where TEntity : class
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
                //pageData.Items = Connection.Query<TEntity>(sql.ToString(), parameters, commandType: CommandType.Text).ToList();

                pageData.Items = this.GetUnitOfWork(readOnly).Query<TEntity>(sql.ToString(), parameters).ToList();
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
        public int GetPageCount(string sqlDistinct, DynamicParameters parameters = null)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT COUNT(1) FROM (select * from (");
            sqlQuery.AppendFormat(sqlDistinct.ToString());
            sqlQuery.AppendFormat(") tp) AS Distinct1");
            return this.GetUnitOfWork(true).Query<int>(sqlQuery.ToString(), parameters).FirstOrDefault();
        }

    }
}