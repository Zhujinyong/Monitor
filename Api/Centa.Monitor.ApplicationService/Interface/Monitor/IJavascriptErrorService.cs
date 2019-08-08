using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.Dto.Monitor.JavascriptError;

namespace Centa.Monitor.ApplicationService.Interface.Monitor
{
    public interface IJavascriptErrorService
    {
        long Add(JavascriptErrorModel model);

        PageDataView<JavascriptErrorListDto> GetList(JavascriptErrorSearchDto dto);
    }
}
