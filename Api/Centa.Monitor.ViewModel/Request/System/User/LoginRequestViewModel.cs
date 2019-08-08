using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Centa.Monitor.ViewModel.Request.System.User
{
    /// <summary>
    /// 登陆获取token
    /// </summary>
    public class LoginRequestViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [FromQuery(Name = "account")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [FromQuery(Name = "password")]
        public string Password { get; set; }
    }
}
