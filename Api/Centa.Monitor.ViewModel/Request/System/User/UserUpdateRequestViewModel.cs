using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Centa.Monitor.ViewModel.Request.System.User
{
    /// <summary>
    /// 修改用户请求实体
    /// </summary>
   public class UpdateUserRequestViewModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [FromQuery(Name = "keyId")]
        [Required]
        public Guid KeyId { get; set; }

        /// <summary>
        /// 域账号
        /// </summary>
        [FromQuery(Name = "domainAccount")]
        public string DomainAccount { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [FromQuery(Name = "realName")]
        public string RealName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [FromQuery(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        [FromQuery(Name = "isUse")]
        public int IsUse { get; set; }


    }
}
