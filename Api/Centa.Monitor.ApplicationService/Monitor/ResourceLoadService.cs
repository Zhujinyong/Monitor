using static Centa.Monitor.Common.CommonHelper;
using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Text;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Dapper;
using System;
using Centa.Monitor.ApplicationService.Interface.Monitor;
using Centa.Monitor.Dto.Monitor.ResourceLoad;

namespace Centa.Monitor.ApplicationService.Monitor
{
    public class ResourceLoadService : IResourceLoadService
    {
        private readonly IRepository<ResourceLoadModel> _repository;

        private IUnitOfWork _unitOfWork;

        public ResourceLoadService(IRepository<ResourceLoadModel> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public long Add(ResourceLoadModel model)
        {
           var result= Execute<long>(() =>
            {
                model.KeyId = Guid.NewGuid();
                model.CreatedAt = model.UpdatedAt = DateTime.Now;
                return _repository.Add(model);
            });
            return result.Data;
        }

        public PageDataView<ResourceLoadListDto> GetList(ResourceLoadSearchDto dto)
        {
            PageDataView<ResourceLoadListDto> result = new PageDataView<ResourceLoadListDto>() { };
            StringBuilder select = new StringBuilder();
            StringBuilder where = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            select.Append("SELECT B.* ,P.ProjectName FROM ResourceLoad AS B JOIN Project AS P ON B.webMonitorId=P.KeyId  ");
            where.Append("WHERE 1=1 ");
            if (!string.IsNullOrEmpty(dto.WebMonitorId))
            {
                where.Append($"AND  WebMonitorId = @WebMonitorId ");
                parameters.Add("@WebMonitorId", dto.WebMonitorId);
            }
            if (!string.IsNullOrEmpty(dto.CustomerKey))
            {
                where.Append($"AND  (B.CustomerKey = @CustomerKey OR B.UserID = @CustomerKey) ");
                parameters.Add("@CustomerKey", dto.CustomerKey);
            }
            if (dto.StartTime.HasValue)
            {
                where.Append($"AND  B.createdAt >= @StartTime ");
                parameters.Add("@StartTime", dto.StartTime);
            }
            if (dto.EndTime.HasValue)
            {
                where.Append($"AND  B.createdAt <= @EndTime ");
                parameters.Add("@EndTime", dto.EndTime);
            }
            Dictionary<string, bool> ordery = new Dictionary<string, bool>();
            ordery.Add("createdAt", false);
            result = _unitOfWork.GetPageData<ResourceLoadListDto>(select.ToString() + where.ToString(), ordery, dto.PageIndex, dto.PageSize, parameters);
            return result;
        }

    }
}
