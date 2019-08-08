using Microsoft.AspNetCore.Mvc;

namespace Centa.Monitor.ViewModel.Request.System.Project
{
   public class ProjectRequestViewModel
    {
        /// <summary>
        /// @Order=3,页码，默认值1，如果传值-1，会返回所有记录
        /// </summary>
        [FromQuery(Name = "pageIndex")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// @Order=4,每页多少条记录，默认10
        /// </summary>
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// @Order=5,项目名称
        /// </summary>
        [FromQuery(Name = "projectName")]
        public string ProjectName { get; set; }
    }
}
