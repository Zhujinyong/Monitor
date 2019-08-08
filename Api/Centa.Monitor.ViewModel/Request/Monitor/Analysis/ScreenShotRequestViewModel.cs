using Microsoft.AspNetCore.Mvc;
using System;

namespace Centa.Monitor.ViewModel.Request.System.Project
{
   public class ScreenShotRequestViewModel
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
        /// @Order=5,项目主键
        /// </summary>
        [FromQuery(Name = "WebMonitorId")]
        public string WebMonitorId { get; set; }

        /// <summary>
        /// @Order=6,用户主键
        /// </summary>
        [FromQuery(Name = "CustomerKey")]
        public string CustomerKey { get; set; }

        /// <summary>
        /// @Order=7,开始时间
        /// </summary>
        [FromQuery(Name = "startTime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// @Order=8,结束时间
        /// </summary>
        [FromQuery(Name = "endTime")]
        public DateTime? EndTime { get; set; }
    }
}
