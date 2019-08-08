using Dapper.Contrib.Extensions;
using System;

namespace Centa.Monitor.Infrastructure.Model.System
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table("[dbo].[User]")]
    public class UserModel : BaseModel
    {
        public string DomainAccount { get; set; }

        public string RealName { get; set; }

        public string Description { get; set; }

        public int IsUse { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}