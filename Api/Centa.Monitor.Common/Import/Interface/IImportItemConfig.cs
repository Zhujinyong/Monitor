using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Common.Import.Interface
{
    /// <summary>
    /// 导入项目的相关配置
    /// </summary>
    public interface IImportItemConfig
    {
        string DataTableName { get; }

        /// <summary>
        /// 数据源中的列
        /// </summary>
        List<string> HeaderList { get; }

        /// <summary>
        /// 需要大于零的列
        /// </summary>
        List<string> GreaterThen0List { get; }

        /// <summary>
        /// 自定义规则
        /// </summary>
        Func<DataRow, string> Rule { get; }

        /// <summary>
        /// 对应表变量的列
        /// </summary>
        Dictionary<string, Type> TableTypeColumns { get; }

        /// <summary>
        /// 遍历数据时，每行执行的方法，主要用来把数据放到表变量里
        /// </summary>
        Action<DataTable, DataRow> EachRowAction { get; }
    }
}
