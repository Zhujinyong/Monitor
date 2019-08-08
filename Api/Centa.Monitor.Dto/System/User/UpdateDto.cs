using System;

namespace Centa.Monitor.Dto.System.User
{
    /// <summary>
    /// 编辑用户信息实体
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid KeyId { get; set; }

        public string DomainAccount { get; set; }

        public string RealName { get; set; }

        public string Description { get; set; }

        public int IsUse { get; set; }
    }
}
