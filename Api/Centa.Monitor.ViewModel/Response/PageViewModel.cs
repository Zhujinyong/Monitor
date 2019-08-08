
namespace Centa.Monitor.ViewModel.Response
{
    /// <summary>
    /// 分页
    /// </summary>
    public class PageViewModel
    {
        /// <summary>
        /// 每页多少条数据
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总共多少条数据
        /// </summary>
        public int Total { get; set; }
    }
}