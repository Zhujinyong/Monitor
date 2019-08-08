using Centa.Monitor.Infrastructure.Enum;
using Centa.Monitor.Infrastructure.Model.System;
using System;

namespace Centa.Monitor.WebApi.ModelBinder
{
    /// <summary>
    /// token模型，注意：属性名称不要和action,controller参数相同
    /// </summary>
    public class TokenModel :UserModel
    {
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ValidateTo { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }
    }
}
