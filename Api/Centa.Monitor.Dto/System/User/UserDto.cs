using System;

namespace Centa.Monitor.Dto.System.User
{
    public class UserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public object KeyId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 域账号
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 所属角色keyid
        /// </summary>
        public Guid UserRoleKeyId { get; set; }

        /// <summary>
        /// 所属公司keyid
        /// </summary>
        public Guid OwnerSubCompanyKeyId { get; set; }

        /// <summary>
        /// 是否是超级管理员 0：否 1：是
        /// </summary>
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// 状态是否启用 0：否 1：是
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
    }
}
