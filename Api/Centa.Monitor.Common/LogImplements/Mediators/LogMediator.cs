using Centa.CCME.Common.LogBImplements.Interfaces;
using Centa.CCME.Common.LogBImplements.Models.Entities;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Centa.CCME.Common.LogBImplements.Mediators
{
    /// <summary>
    /// 日志中介者
    /// </summary>
    public static class LogMediator
    {
        private static readonly Type _LoggerImplType;
        static LogMediator()
        {
            Assembly impAssembly = Assembly.Load(LoggerProviderConfiguration.Setting.Assembly);
            LogMediator._LoggerImplType = impAssembly.GetType(LoggerProviderConfiguration.Setting.Type);

        }
        public static Guid Write(ExceptionLog log)
        {
                ILoggger loggger = (ILoggger)Activator.CreateInstance(LogMediator._LoggerImplType);
                Guid logId = loggger.Write(log);
                return logId;
        }
        public static Guid Write(RunningLog log)
        {
            ILoggger loggger = (ILoggger)Activator.CreateInstance(LogMediator._LoggerImplType);
            Guid logId = loggger.Write(log);

            return logId;
        }
        public static async Task<Guid> WriteAsync(ExceptionLog log)
        {
            ILoggger loggger = (ILoggger)Activator.CreateInstance(LogMediator._LoggerImplType);
            Task<Guid> logId = Task.Run(() => loggger.Write(log));

            return await logId;
        }
        public static async Task<Guid> WriteAsync(RunningLog log)
        {
            ILoggger loggger = (ILoggger)Activator.CreateInstance(LogMediator._LoggerImplType);
            Task<Guid> logId = Task.Run(() => loggger.Write(log));

            return await logId;
        }
    }
}
