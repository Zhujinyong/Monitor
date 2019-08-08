using System;

namespace Centa.CCME.Common.LogBImplements.Models.Entities
{
    /// <summary>
    /// 日志基类
    /// </summary>
    [Serializable]
    public abstract class BaseLog
    {
        public Guid Id { get; set; }
        // 命名空间
        public string Namespace { get; set; }
        // 类名
        public string ClassName { get; set; }
        // 方法名
        public string MethodName { get; set; }
        // 方法类型
        public string MethodType { get; set; }
        // 方法参数Json
        public string ArgsJson { get; set; }
        // IP地址
        public string IPAddress { get; set; }
    }
}
