using Centa.Monitor.ApplicationService.Interface.System;
using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Interfaces;
using Centa.Monitor.Infrastructure.Model.System.Project;
using System.Collections.Generic;
using System;
using System.Text;
using Centa.Monitor.Dto.System.Project;
using Centa.Monitor.Infrastructure.Model.System;
using AutoMapper;
using Dapper;

namespace Centa.Monitor.ApplicationService.System
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<ProjectModel> _ProjectRepository;

        private IUnitOfWork _unitOfWork;

        public ProjectService(IRepository<ProjectModel> ProjectRepository, 
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ProjectRepository = ProjectRepository;
        }

        public PageDataView<ProjectListDto> GetList(SearchProjectDto dto)
        {
            PageDataView<ProjectListDto> result = new PageDataView<ProjectListDto>() { };
            StringBuilder select = new StringBuilder();
            StringBuilder where = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            select.Append("SELECT * FROM Project  ");
            where.Append("WHERE 1=1 ");
            if (!string.IsNullOrEmpty(dto.ProjectName))
            {
                where.Append($"AND  ProjectName LIKE @ProjectName ");
                parameters.Add("@ProjectName", "%" + dto.ProjectName + "%");
            }
            Dictionary<string, bool> ordery = new Dictionary<string, bool>();
            ordery.Add("createdAt", false);
            result = _unitOfWork.GetPageData<ProjectListDto>(select.ToString() + where.ToString(), ordery, dto.PageIndex, dto.PageSize, parameters);
            return result;
        }

        public void Add(AddProjectDto dto)
        {
            var ProjectModel = Mapper.Map<ProjectModel>(dto);
            ProjectModel.KeyId = Guid.NewGuid();
            ProjectModel.CreatedAt = ProjectModel.UpdatedAt = DateTime.Now;
            _ProjectRepository.Add(ProjectModel);
        }
        
        public void Update(UpdateProjectDto dto)
        {
            var ProjectModel = _ProjectRepository.GetByKeyId(dto.KeyId);
            var createTime=ProjectModel.CreatedAt;
            ProjectModel = Mapper.Map<ProjectModel>(dto);
            ProjectModel.UpdatedAt = DateTime.Now;
            ProjectModel.CreatedAt = createTime;
            _ProjectRepository.Update(ProjectModel);
           
        }
        
        public void Delete(DeleteProjectDto dto)
        {
            var ProjectModel = _ProjectRepository.GetByKeyId(dto.KeyId);
            _ProjectRepository.Remove(ProjectModel);
        }
    }
}