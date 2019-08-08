using System;

namespace Centa.CCME.Common.LogBImplements.Models.Entities
{
    /// <summary>
    /// 运行日志
    /// </summary>
    [Serializable]
    public class RunningLog : BaseLog
    {
        // 方法返回值
        public string ReturnValue { get; set; }
        // 方法返回值
        public string ReturnValueType { get; set; }
        // 操作人账号
        public string OperatorAccount { get; set; }
        // 开始时间
        public DateTime StartTime { get; set; }
        // 结束时间
        public DateTime EndTime { get; set; }
    }
}
