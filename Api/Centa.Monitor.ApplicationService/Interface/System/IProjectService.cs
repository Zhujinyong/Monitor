using Centa.Monitor.Infrastructure.Model.System.Project;
using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Dto.System.Project;

namespace Centa.Monitor.ApplicationService.Interface.System
{
    public interface IProjectService
    {
        PageDataView<ProjectListDto> GetList(SearchProjectDto dto);

        void Add(AddProjectDto dto);

        void Update(UpdateProjectDto dto);

        void Delete(DeleteProjectDto dto);

    }
}
