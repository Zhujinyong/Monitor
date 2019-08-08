using static Centa.Monitor.Common.CommonHelper;
using Centa.Monitor.Infrastructure.General.Page;
using Centa.Monitor.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Text;
using Centa.Monitor.Infrastructure.Model.Monitor;
using Dapper;
using System;
using Centa.Monitor.ApplicationService.Interface.Monitor;
using System.IO;
using Centa.Monitor.Dto.Monitor.ScreenShot;

namespace Centa.Monitor.ApplicationService.Monitor
{
    public class ScreenShotService : IScreenShotService
    {
        private readonly IRepository<ScreenShotModel> _repository;

        private IUnitOfWork _unitOfWork;

        public ScreenShotService(IRepository<ScreenShotModel> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public long Add(ScreenShotModel model)
        {
            model.KeyId = Guid.NewGuid();
            model.CreatedAt = model.UpdatedAt = DateTime.Now;
            #region 保存截图
            var imageResult=Execute<string>(() => {
                var savePath = $"//Monitor//Screen//{model.CreatedAt.Year}//{model.CreatedAt.Month}//{model.CreatedAt.Day}//{model.KeyId}.jpeg";
                string path = $"{Directory.GetCurrentDirectory()}//wwwroot//{savePath}";
                string filepath = Path.GetDirectoryName(path);
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string dummyData = model.ScreenInfo.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
                if (dummyData.Length % 4 > 0)
                {
                    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
                }
                var photoBytes = Convert.FromBase64String(dummyData);
                File.WriteAllBytes(path, photoBytes);
                return savePath;
            });
            if(imageResult.Status==200)
            {
                model.ScreenInfo = imageResult.Data;
            }
            #endregion
            var result = Execute<long>(() =>
            {
                return _repository.Add(model);
            });
            return result.Data;

        }

        public PageDataView<ScreenShotListDto> GetList(ScreenShotSearchDto dto)
        {
            PageDataView<ScreenShotListDto> result = new PageDataView<ScreenShotListDto>() { };
            StringBuilder select = new StringBuilder();
            StringBuilder where = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            select.Append("SELECT B.* ,P.ProjectName FROM ScreenShot AS B JOIN Project AS P ON B.webMonitorId=P.KeyId  ");
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
            result = _unitOfWork.GetPageData<ScreenShotListDto>(select.ToString() + where.ToString(), ordery, dto.PageIndex, dto.PageSize, parameters);
            return result;
        }

        public string GetImagePath(Guid keyId)
        {
            var model=_repository.GetByKeyId(keyId);
            var result = string.Empty;
            if(model!=null&& model.ScreenInfo.StartsWith(@"//Monitor"))
            {
                result= $"{Directory.GetCurrentDirectory()}//wwwroot//{model.ScreenInfo}"; ;
            }
            return result;
        }


    }
}
