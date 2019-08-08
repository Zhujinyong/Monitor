
namespace Centa.Monitor.Dto.System.User
{
    /// <summary>
    /// 用户列表查询实体
    /// </summary>
    public class UserSearchDto
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string DomainAccount { get; set; }

        public string RealName { get; set; }
    }
}
