using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Centa.Monitor.Dto.Monitor.ScreenShot;
using System;

namespace Centa.Monitor.ApplicationService.Interface.Monitor
{
    public interface IScreenShotService
    {
        long Add(ScreenShotModel model);

        PageDataView<ScreenShotListDto> GetList(ScreenShotSearchDto dto);

        string GetImagePath(Guid keyId);
    }
}
