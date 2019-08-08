using System;

namespace Centa.Monitor.Infrastructure.Model.System.Project
{
    /// <summary>
    /// 编辑用户信息实体
    /// </summary>
    public class UpdateProjectDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid KeyId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsUse { get; set; }
    }
}
