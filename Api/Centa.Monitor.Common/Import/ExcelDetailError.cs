
namespace Centa.Monitor.Common.Import
{
    /// <summary>
    /// 具体错误：包括行号和错误信息
    /// </summary>
    public class ImportDetailError
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int LineNo { set; get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { set; get; }
    }
}
