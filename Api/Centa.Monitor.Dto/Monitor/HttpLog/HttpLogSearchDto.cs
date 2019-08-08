using System;

namespace Centa.Monitor.Dto.Monitor.HttpLog
{
    public class HttpLogSearchDto
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string WebMonitorId { get; set; }

        public string CustomerKey { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
