using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.Dto.Monitor.HttpLog;

namespace Centa.Monitor.ApplicationService.Interface.Monitor
{
    public interface IHttpLogService
    {
        long Add(HttpLogModel model);

        PageDataView<HttpLogListDto> GetList(HttpLogSearchDto dto);
    }
}
