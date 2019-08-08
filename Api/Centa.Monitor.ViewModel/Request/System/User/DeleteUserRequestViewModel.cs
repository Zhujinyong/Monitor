using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Centa.Monitor.ViewModel.Request.System.User
{
   public class DeleteUserRequestViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [FromQuery(Name = "keyId")]
        [Required]
        public Guid KeyId { get; set; }

    }
}
