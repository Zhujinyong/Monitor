using System;

namespace Centa.CCME.Common.LogBImplements.Attributes
{
    /// <summary>
    /// 跳过AOP记录异常特性
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SkipExceptionAttribute : Attribute
    {

    }
}
