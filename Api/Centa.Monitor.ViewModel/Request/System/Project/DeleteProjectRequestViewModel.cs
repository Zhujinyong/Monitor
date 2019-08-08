using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Centa.Monitor.ViewModel.Request.System.Project
{
   public class DeleteProjectRequestViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [FromQuery(Name = "keyId")]
        [Required]
        public Guid KeyId { get; set; }

    }
}
