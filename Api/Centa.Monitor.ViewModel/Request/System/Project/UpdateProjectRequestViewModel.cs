using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Centa.Monitor.ViewModel.Request.System.Project
{
    /// <summary>
    /// 修改项目请求实体
    /// </summary>
   public class UpdateProjectRequestViewModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [FromQuery(Name = "keyId")]
        [Required]
        public Guid KeyId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [FromQuery(Name = "projectName")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [FromQuery(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [FromQuery(Name = "isUse")]
        public int IsUse { get; set; }

    }
}
