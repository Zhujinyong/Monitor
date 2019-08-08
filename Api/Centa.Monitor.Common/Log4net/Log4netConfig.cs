using System.IO;
using System.Reflection;
using log4net;
using log4net.Repository;

namespace Centa.Monitor.Common.Log4net
{
    /// <summary>
    /// 读取log4net配置文件
    /// </summary>
    public class Log4netConfig
    {
        /// <summary>
        /// log4net日志
        /// </summary>
        public static ILoggerRepository repository { get; set; }

        static Log4netConfig()
        {            
            //加载log4net日志配置文件          
            repository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }
    }
}