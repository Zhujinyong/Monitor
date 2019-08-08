using Dapper.Contrib.Extensions;

namespace Centa.Monitor.Infrastructure.Model.Monitor
{
    /// <summary>
    /// js报错日志
    /// </summary>
    [Table("[dbo].[JavascriptError]")]
    public partial class JavascriptErrorModel : LogBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string PageKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Os { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BrowserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MonitorIp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorStack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BrowserInfo { get; set; }

    }
}

