using Centa.Monitor.Infrastructure.Model.Monitor;

namespace Centa.Monitor.Dto.Monitor.ResourceLoad
{
    /// <summary>
    /// 用户列表实体
    /// </summary>
    public  class ResourceLoadListDto : ResourceLoadModel
    {
        public string ProjectName { get; set; }
    }
}
