
namespace Centa.Monitor.ViewModel.Response
{
    /// <summary>
    /// 有数据的返回格式
    /// </summary>
    public class PageResponseViewModel : DataResponseViewModel
    {
        /// <summary>
        /// 分页
        /// </summary>
        public PageViewModel Page { get; set; }
    }
}