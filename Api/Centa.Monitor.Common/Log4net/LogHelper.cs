﻿using System;
using System.Collections.Concurrent;
using log4net;

namespace Centa.Monitor.Common.Log4net
{
    public static class LogHelper
    {
        private static readonly ConcurrentDictionary<Type, ILog> Loggers = new ConcurrentDictionary<Type, ILog>();

        /// <summary>
        /// 获取记录器
        /// </summary>
        /// <param name="source">soruce</param>
        /// <returns></returns>
        private static ILog GetLogger(Type source)
        {
            if (Loggers.ContainsKey(source))
            {
                return Loggers[source];
            }
            else
            {
                ILog logger = LogManager.GetLogger(Log4netConfig.repository.Name, source);
                Loggers.TryAdd(source, logger);
                return logger;
            }
        }
        
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Debug(object source, string message)
        {
            Debug(source.GetType(), message);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="ps">ps</param>
        public static void Debug(object source, string message, params object[] ps)
        {
            Debug(source.GetType(), string.Format(message, ps));
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Debug(Type source, string message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Info(object source, object message)
        {
            Info(source.GetType(), message);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Info(Type source, object message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Warn(object source, object message)
        {
            Warn(source.GetType(), message);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Warn(Type source, object message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Error(object source, object message)
        {
            Error(source.GetType(), message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Error(Type source, object message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Fatal(object source, object message)
        {
            Fatal(source.GetType(), message);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public static void Fatal(Type source, object message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }
        
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Debug(object source, object message, Exception exception)
        {
            Debug(source.GetType(), message, exception);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Debug(Type source, object message, Exception exception)
        {
            GetLogger(source).Debug(message, exception);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Info(object source, object message, Exception exception)
        {
            Info(source.GetType(), message, exception);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Info(Type source, object message, Exception exception)
        {
            GetLogger(source).Info(message, exception);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Warn(object source, object message, Exception exception)
        {
            Warn(source.GetType(), message, exception);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Warn(Type source, object message, Exception exception)
        {
            GetLogger(source).Warn(message, exception);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Error(object source, object message, Exception exception)
        {
            Error(source.GetType(), message, exception);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Error(Type source, object message, Exception exception)
        {
            GetLogger(source).Error(message, exception);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Fatal(object source, object message, Exception exception)
        {
            Fatal(source.GetType(), message, exception);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public static void Fatal(Type source, object message, Exception exception)
        {
            GetLogger(source).Fatal(message, exception);
        }
    }
}