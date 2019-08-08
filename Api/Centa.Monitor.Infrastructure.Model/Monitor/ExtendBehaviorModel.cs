using Dapper.Contrib.Extensions;
using System;

namespace Centa.Monitor.Infrastructure.Model.Monitor
{
    /// <summary>
    /// 自定义日志
    /// </summary>
    [Table("[dbo].[ExtendBehavior]")]
    public partial class ExtendBehaviorModel 
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ExplicitKey]
        public Guid KeyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid WebMonitorId { get; set; }

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 行为类型
        /// </summary>
        public string BehaviorType { get; set; }

        /// <summary>
        /// 行为结果（成功、失败等）
        /// </summary>
        public string BehaviorResult { get; set; }

        /// <summary>
        /// 日志类型（分类）
        /// </summary>
        public string UploadType { get; set; }


        public string completeUrl { get; set; }

        /// <summary>
        /// 行为描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime HappenTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}

