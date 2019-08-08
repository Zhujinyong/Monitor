using Centa.Monitor.Common.Import.Interface;
using Centa.Monitor.Common.Interface;
using System.Data;

namespace Centa.Monitor.Common.Import
{
    /// <summary>
    /// 按导入项目的相关配置执行导入
    /// </summary>
    public class ImportItemConfig 
    {
        public IResult<ImportResult> Import(IImportItemConfig importItem,DataTable dataTable, ImportType importType)
        {
            return new ImportDataTable()
                     .WithDataTable(dataTable)
                     .WithHeaderList(importItem.HeaderList)
                     .WithGreaterThen0List(importItem.GreaterThen0List)
                     .WithRule(importItem.Rule)
                     .WithFormatDataTableName(importItem.DataTableName)
                     .WithTableTypeColumns(importItem.TableTypeColumns)
                     .WithEachRowAction(importItem.EachRowAction)
                     .Import(importType);
        }
    }
}
