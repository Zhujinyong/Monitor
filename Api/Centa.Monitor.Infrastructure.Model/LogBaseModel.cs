using System;

namespace Centa.Monitor.Infrastructure.Model
{
    public abstract class LogBaseModel : BaseModel
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public string UploadType { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappenTime { get; set; }

        /// <summary>
        /// 监控ID
        /// </summary>
        public Guid WebMonitorId { get; set; }

        /// <summary>
        /// 用户标识ID
        /// </summary>
        public string CustomerKey { get; set; }

        /// <summary>
        /// 发生的页面URL
        /// </summary>
        public string SimpleUrl { get; set; }

        /// <summary>
        /// 发生的页面完整的URL
        /// </summary>
        public string CompleteUrl { get; set; }

        /// <summary>
        /// 自定义用户标识ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 自定义用户参数1
        /// </summary>
        public string FirstUserParam { get; set; }

        /// <summary>
        /// 自定义用户参数2
        /// </summary>
        public string SecondUserParam { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
