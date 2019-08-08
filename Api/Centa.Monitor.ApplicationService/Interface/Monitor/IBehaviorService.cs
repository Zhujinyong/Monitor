using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.Dto.Monitor.Behavior;

namespace Centa.Monitor.ApplicationService.Interface.Monitor
{
    public interface IBehaviorService
    {
        long Add(BehaviorModel behavior);

        PageDataView<BehaviorListDto> GetList(BehaviorSearchDto dto);
    }
}
