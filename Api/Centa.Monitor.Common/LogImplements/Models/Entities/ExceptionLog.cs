using System;

namespace Centa.CCME.Common.LogBImplements.Models.Entities
{
    /// <summary>
    /// 异常日志
    /// </summary>
    [Serializable]
    public class ExceptionLog : BaseLog
    {
        // 异常类型
        public string ExceptionType { get; set; }
        // 异常消息
        public string ExceptionMessage { get; set; }
        // 异常详细信息
        public string ExceptionInfo { get; set; }
        // 内部异常
        public string InnerException { get; set; }
        // 异常发生时间
        public DateTime OccurredTime { get; set; }
    }
}
