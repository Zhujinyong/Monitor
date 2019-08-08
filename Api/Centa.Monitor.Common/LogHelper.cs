using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace Centa.Monitor.Common
{
    public class LogHelper
    {
        private static ILog logger;

        static LogHelper()
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            logger = LogManager.GetLogger(repository.Name, "NETCorelog4net");
        }


        public static void Error(object message, Exception ex)
        {
            logger.Error(message, ex);
        }


        public static void Error(object message)
        {
            logger.Error("/r/n"+message);
        }

        public static void Debug(object message)
        {
            logger.Debug(message);
        }

        public static void Warn(object message)
        {
            logger.Warn(message);
        }

        public static void Info(object message, Exception ex)
        {
            logger.Error(message, ex);
        }

        public static void Info(object message)
        {
            logger.Info("\n" + message+ "\n");
        }

        
    }
}
