
namespace Centa.Monitor.Infrastructure.Model.System.Project
{
    /// <summary>
    /// 用户列表查询实体
    /// </summary>
    public class SearchProjectDto
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string ProjectName { get; set; }
    }
}
