using Centa.Monitor.Common.Interface;

namespace Centa.Monitor.Common
{
    public class Result<T> : IResult<T>
    {
        public int Status { get; private set; }

        public string Message { get; private set; }

        public T Data { get; private set; }

        public Result(int status = 200, string message = "成功", T data = default(T))
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
