using Centa.CCME.Common.LogBImplements.Interfaces;
using Centa.CCME.Common.LogBImplements.Models.Entities;
using Centa.CCME.Common.LogBImplements.Toolkits;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Centa.CCME.Common.LogBImplements.Implements
{
    /// <summary>
    /// 日志记录者默认实现
    /// </summary>
    public class DefaultLogger : ILoggger
    {
        private static RedisHelper _redisrepository;
        public   DefaultLogger()
        {
            if (_redisrepository == null)
            {
                _redisrepository = new RedisHelper();
            }
        }
        private static readonly string Elog = "ExceptionLog.";
        private static readonly string Rlog = "RunningLog.";
        public Guid Write(ExceptionLog log)
        {
            try
            {
                log.Id = Guid.NewGuid();
                string Key = Elog + DateTime.Now.ToString("yyyyMMdd");
                bool _KeyExists = _redisrepository.KeyExists(Key);

                _redisrepository.HashSet(Key, log.Id.ToString(), log);
                if (!_KeyExists)
                {
                    _redisrepository.KeyExpire(Key, TimeSpan.FromDays(10));
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex.ToString());
            }
            return log.Id;
        }
        public Guid Write(RunningLog log)
        {
            try
            {
                log.Id = Guid.NewGuid();
                string Key = Rlog+DateTime.Now.ToString("yyyyMMdd");
                bool _KeyExists = _redisrepository.KeyExists(Key);
                _redisrepository.HashSet(Key, log.Id.ToString(), log);
                if (!_KeyExists)
                {
                    _redisrepository.KeyExpire(Key, TimeSpan.FromDays(10));
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex.ToString());
            }
            return log.Id;
        }
    }
}
