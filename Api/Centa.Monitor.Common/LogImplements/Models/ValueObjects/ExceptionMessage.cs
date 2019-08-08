using System;

namespace Centa.CCME.Common.LogBImplements.Models.ValueObjects
{
    /// <summary>
    /// 异常消息
    /// </summary>
    [Serializable]
    public struct ExceptionMessage
    {
        public ExceptionMessage(string errorMessage, Guid logId)
        {
            this.ErrorMessage = errorMessage;
            this.LogId = logId;
        }
        public string ErrorMessage;
        public Guid LogId;
    }
}
