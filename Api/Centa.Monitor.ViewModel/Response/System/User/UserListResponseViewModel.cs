using System;

namespace Centa.Monitor.ViewModel.Response.System.User
{
    /// <summary>
    /// 用户列表返回信息
    /// </summary>
   public class UserListResponseViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string KeyId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int Version1 { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }
        
        /// <summary>
        /// 所属角色
        /// </summary>
        public string RoleName { get; set; }


        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid? UserRoleKeyId { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid? OwnerSubCompanyKeyId { get; set; }

        /// <summary>
        /// 所属分公司
        /// </summary>
        public string SubCompanyName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Status { get; set; }
    }
}
