using Dapper.Contrib.Extensions;

namespace Centa.Monitor.Infrastructure.Model.Monitor
{
    /// <summary>
    /// 用户的行为
    /// </summary>
    [Table("[dbo].[behavior]")]
    public partial class BehaviorModel : LogBaseModel
    {
        /// <summary>
        /// 行为类型
        /// </summary>
        public string BehaviorType { get; set; }

        /// <summary>
        /// 元素的类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Input框的placeholder
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// 输入的内容
        /// </summary>
        public string InputValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InnerText { get; set; }
    }
}

