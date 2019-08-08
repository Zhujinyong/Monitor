using System;

namespace Centa.CCME.Common.LogBImplements.Models.ValueObjects
{
    /// <summary>
    /// 方法参数
    /// </summary>
    [Serializable]
    internal struct MethodArg
    {
        public MethodArg(string name, string type, object val)
        {
            this.Name = name;
            this.Type = type;
            this.Value = val;
        }
        public string Name;
        public string Type;
        public object Value;
    }
}
