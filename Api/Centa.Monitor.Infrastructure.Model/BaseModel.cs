using Dapper.Contrib.Extensions;
using System;

namespace Centa.Monitor.Infrastructure.Model
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ExplicitKey]
        public Guid KeyId { get; set; }
    }
}
