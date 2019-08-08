using Centa.Monitor.ApplicationService.Interface;
using static Centa.Monitor.Common.CommonHelper;
using static Centa.Monitor.Common.LogJsonHelper;
using static Centa.Monitor.Common.Extensions;
using Centa.Monitor.ApplicationService.Interface.Monitor;
using Centa.Monitor.Dto.Monitor.Behavior;
using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Interfaces;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Centa.Monitor.ApplicationService.Monitor
{
    /// <summary>
    /// 
    /// </summary>
    public class BehaviorService : IBehaviorService
    {
        private readonly IRepository<BehaviorModel> _repository;

        private IUnitOfWork _unitOfWork;

        public BehaviorService(IRepository<BehaviorModel> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public long Add(BehaviorModel model)
        {
            var result = Execute<long>(() =>
            {
                model.KeyId = Guid.NewGuid();
                model.CreatedAt = model.UpdatedAt = DateTime.Now;
                return _repository.Add(model);
            });
            return result.Data;
        }

        public PageDataView<BehaviorListDto> GetList(BehaviorSearchDto dto)
        {
            PageDataView<BehaviorListDto> result = new PageDataView<BehaviorListDto>() { };
            StringBuilder select = new StringBuilder();
            StringBuilder where = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            select.Append("SELECT B.* ,P.ProjectName FROM Behavior AS B JOIN Project AS P ON B.webMonitorId=P.KeyId  ");
            where.Append("WHERE 1=1 ");
            if (!string.IsNullOrEmpty(dto.WebMonitorId))
            {
                where.Append($"AND  B.WebMonitorId = @WebMonitorId ");
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
            result = _unitOfWork.GetPageData<BehaviorListDto>(select.ToString() + where.ToString(), ordery, dto.PageIndex, dto.PageSize, parameters);
            return result;
        }

    }
}
