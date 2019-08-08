using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Centa.Monitor.ViewModel.Request.System.Project
{
   public class AddProjectRequestViewModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [FromQuery(Name = "projectName")]
        [Required]
        public string ProjectName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [FromQuery(Name = "description")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        [FromQuery(Name = "isUse")]
        [Required]
        public int IsUse { get; set; }
    }
}
