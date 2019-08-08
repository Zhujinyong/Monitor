using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.Dto.Monitor.ExtendBehavior;

namespace Centa.Monitor.ApplicationService.Interface.Monitor
{
    public interface IExtendBehaviorService
    {
        long Add(ExtendBehaviorModel model);

        PageDataView<ExtendBehaviorListDto> GetList(ExtendBehaviorSearchDto dto);
    }
}
