using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.Dto.Monitor.LoadPage;

namespace Centa.Monitor.ApplicationService.Interface.Monitor
{
    public interface ILoadPageService
    {
        long Add(LoadPageModel model);

        PageDataView<LoadPageListDto> GetList(LoadPageSearchDto dto);
    }
}
