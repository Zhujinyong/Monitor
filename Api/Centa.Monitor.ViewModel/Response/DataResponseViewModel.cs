
namespace Centa.Monitor.ViewModel.Response
{
    /// <summary>
    /// 单个数据的返回格式
    /// </summary>
    public class DataResponseViewModel : ResponseViewModel
    {
        /// <summary>
        /// 单个数据
        /// </summary>
        public object Data { get; set; }
    }
}
