using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.Dto.Monitor.CustomerPV;

namespace Centa.Monitor.ApplicationService.Interface.Monitor
{
    public interface ICustomerPVService
    {
        long Add(CustomerPVModel model);

        PageDataView<CustomerPVListDto> GetList(CustomerPVSearchDto dto);
    }
}
