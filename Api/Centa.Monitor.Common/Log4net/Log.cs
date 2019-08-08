using System;

namespace Centa.Monitor.Common.Log4net
{
    public static class Log
    {
        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void WriteLog(object source, string message)
        {
            LogHelper.Info(source, message);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void WriteLog(Type type, string message)
        {
            LogHelper.Info(type, message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void WriteLogError(object source, string message)
        {
            LogHelper.Error(source, message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public static void WriteLogError(Type type, string message)
        {
            LogHelper.Error(type, message);
        }
    }
}