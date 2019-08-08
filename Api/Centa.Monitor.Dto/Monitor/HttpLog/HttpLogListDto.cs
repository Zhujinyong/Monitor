using Centa.Monitor.Infrastructure.Model.Monitor;

namespace Centa.Monitor.Dto.Monitor.HttpLog
{
    /// <summary>
    /// 用户列表实体
    /// </summary>
    public  class HttpLogListDto : HttpLogModel
    {
        public string ProjectName { get; set; }
    }
}
