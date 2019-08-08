using Dapper.Contrib.Extensions;

namespace Centa.Monitor.Infrastructure.Model.Monitor
{
    /// <summary>
    /// 用户访问日志
    /// </summary>
    [Table("[dbo].[CustomerPV]")]
    public partial class CustomerPVModel : LogBaseModel
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
        /// 加载类型(首次加载或是reload)
        /// </summary>
        public string LoadType { get; set; }

        /// <summary>
        /// 加载时间
        /// </summary>
        public string LoadTime { get; set; }
    }
}

