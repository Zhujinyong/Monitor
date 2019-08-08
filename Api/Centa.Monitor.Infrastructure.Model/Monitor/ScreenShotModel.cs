using Dapper.Contrib.Extensions;

namespace Centa.Monitor.Infrastructure.Model.Monitor
{
    /// <summary>
    /// 用户加载页面信息
    /// </summary>
    [Table("[dbo].[LoadPage]")]
    public partial class LoadPageModel : LogBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string LoadPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DomReady { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LookupDomain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ttfb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoadEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Appcache { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UnloadEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CONNECT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoadType { get; set; }
    }
}

