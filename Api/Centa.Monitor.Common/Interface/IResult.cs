
namespace Centa.Monitor.Common.Interface
{
    /// <summary>
    /// 结果
    /// </summary>
    public interface IResult<T>
    {
        /// <summary>
        /// 200成功，其他错误:801执行方法出错
        /// </summary>
        int Status { get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 数据
        /// </summary>
        T Data { get; }
    }
}
