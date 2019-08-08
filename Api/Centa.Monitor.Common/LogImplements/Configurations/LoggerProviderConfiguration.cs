using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace Centa.CCME.Common.LogBImplements
{
    /// <summary>
    /// 日志记录者配置
    /// </summary>
    public class LoggerProviderConfiguration : ConfigurationSection
    {
        private static readonly LoggerProviderConfiguration _Setting;
        static LoggerProviderConfiguration()
        {
            LoggerProviderConfiguration._Setting = (LoggerProviderConfiguration)ConfigurationManager.GetSection("loggerProviderConfiguration");
            if (LoggerProviderConfiguration._Setting == null)
            {
                throw new ApplicationException("日志记录者节点未配置，请检查程序！");
            }
        }
        public static LoggerProviderConfiguration Setting
        {
            get { return LoggerProviderConfiguration._Setting; }
        }
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return this["type"].ToString(); }
            set { this["type"] = value; }
        }
        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return this["assembly"].ToString(); }
            set { this["assembly"] = value; }
        }
    }
}
