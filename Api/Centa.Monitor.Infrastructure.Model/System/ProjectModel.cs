using Dapper.Contrib.Extensions;
using System;

namespace Centa.Monitor.Infrastructure.Model.System
{
    /// <summary>
    /// 项目
    /// </summary>
    [Table("[dbo].[Project]")]
    public class ProjectModel : BaseModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsUse { get; set; }
    }
}