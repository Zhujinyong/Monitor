using Centa.CCME.Common.LogBImplements.Models.Entities;
using System;

namespace Centa.CCME.Common.LogBImplements.Interfaces
{
    /// <summary>
    /// 日志记录者接口
    /// </summary>
    public interface ILoggger
    {
        Guid Write(ExceptionLog log);
        Guid Write(RunningLog log);
    }
}
