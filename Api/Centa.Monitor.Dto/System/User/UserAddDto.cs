
namespace Centa.Monitor.Dto.System.User
{
    /// <summary>
    /// 新增用户实体
    /// </summary>
    public  class UserAddDto
    {
        public string DomainAccount { get; set; }

        public string RealName { get; set; }

        public string Description { get; set; }

        public int IsUse { get; set; }
    }
}
