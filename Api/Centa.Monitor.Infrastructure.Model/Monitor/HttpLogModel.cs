using Dapper.Contrib.Extensions;

namespace Centa.Monitor.Infrastructure.Model.Monitor
{
    /// <summary>
    /// 接口日志
    /// </summary>
    [Table("[dbo].[HttpLog]")]
    public partial class HttpLogModel : LogBaseModel
    {
        /// <summary>
        /// 接口请求的完整URL
        /// </summary>
        public string HttpUrl { get; set; }

        /// <summary>
        /// 接口请求的简洁URL
        /// </summary>
        public string SimpleHttpUrl { get; set; }

        /// <summary>
        /// 接口状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 接口状态描述
        /// </summary>
        public string StatusText { get; set; }

        /// <summary>
        /// 接口结果状态
        /// </summary>
        public string StatusResult { get; set; }
    }
}

