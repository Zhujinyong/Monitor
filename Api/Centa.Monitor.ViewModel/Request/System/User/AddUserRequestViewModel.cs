using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Centa.Monitor.ViewModel.Request.System.User
{
    /// <summary>
    /// 保存用户请求实体
    /// </summary>
   public class AddUserRequestViewModel
    {
        /// <summary>
        /// 域账号
        /// </summary>
        [FromQuery(Name = "domainAccount")]
        [Required]
        public string DomainAccount { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [FromQuery(Name = "realName")]
        [Required]
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
        [Required]
        public int IsUse { get; set; }
    }
}
