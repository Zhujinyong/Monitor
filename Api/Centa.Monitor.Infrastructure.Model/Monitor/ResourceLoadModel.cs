using Dapper.Contrib.Extensions;

namespace Centa.Monitor.Infrastructure.Model.Monitor
{
    /// <summary>
    /// 静态资源
    /// </summary>
    [Table("[dbo].[ResourceLoad]")]
    public partial class ResourceLoadModel : LogBaseModel
    {
        /// <summary>
        /// 静态资源的请求路径
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// 静态资源的类型
        /// </summary>
        public string ElementType { get; set; }

        /// <summary>
        /// 资源加载状态
        /// </summary>
        public string Status { get; set; }
    }
}

