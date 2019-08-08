using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.Dto.Monitor.ResourceLoad;

namespace Centa.Monitor.ApplicationService.Interface.Monitor
{
    public interface IResourceLoadService
    {
        long Add(ResourceLoadModel model);

        PageDataView<ResourceLoadListDto> GetList(ResourceLoadSearchDto dto);
    }
}
