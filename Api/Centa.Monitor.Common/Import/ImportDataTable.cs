using Centa.Monitor.Common.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Centa.Monitor.Common.CommonHelper;

namespace Centa.Monitor.Common.Import
{
    /// <summary>
    /// 导入数据
    /// </summary>
    public class ImportDataTable
    {
        #region private
        /// <summary>
        /// 表头
        /// </summary>
        private List<string> _headerList;

        /// <summary>
        /// 大于0的列
        /// </summary>
        private List<string> _greaterThen0List;

        /// <summary>
        /// 自定义规则
        /// </summary>
        private Func<DataRow, string> _dataRowRule;

        private DataTable _formatDataTable = new DataTable();

        private Action<DataTable, DataRow> _eachRowAction;

        private DataTable _dataTable;
        #endregion

        #region With
        public ImportDataTable WithTableTypeColumns(Dictionary<string, System.Type> columns)
        {
            foreach (var column in columns)
            {
                _formatDataTable.Columns.Add(column.Key, column.Value);
            }
            return this;
        }

        public ImportDataTable WithEachRowAction(Action<DataTable, DataRow> eachRowAction)
        {
            _eachRowAction = eachRowAction;
            return this;
        }

        public ImportDataTable WithHeaderList(List<string> headerList)
        {
            _headerList = headerList;
            return this;
        }

        public ImportDataTable WithFormatDataTableName(string tableName)
        {
            _formatDataTable.TableName = tableName;
            return this;
        }

        public ImportDataTable WithRule(Func<DataRow, string> dataRowRule)
        {
            _dataRowRule = dataRowRule;
            return this;
        }

        public ImportDataTable WithGreaterThen0List(List<string> greaterThen0List)
        {
            _greaterThen0List = greaterThen0List;
            return this;
        }

        public ImportDataTable WithDataTable(DataTable dataTable)
        {
            _dataTable = dataTable;
            return this;
        }
        #endregion

        #region Import
        public IResult<ImportResult> Import(ImportType importType)
        {
            return Execute<ImportResult>(() =>
            {
                var result = new ImportResult() { ErrorList = new List<ImportDetailError>() };
                if (_dataTable == null || _headerList == null)
                {
                    throw new Exception("_dataTable或_headerList不能为空");
                }
                else if (_dataTable.Rows.Count < 2)
                {
                    throw new Exception("导入数据出错,模板中没有任何导入数据");
                }
                else if (_dataTable.Columns.Count != _headerList.Count)
                {
                    throw new Exception("导入数据出错,模板格式不正确");
                }
                else if (_dataTable.Rows[0].ItemArray.Any(p => p == DBNull.Value))
                {
                    throw new Exception("导入数据出错,模板表头不能为空");
                }

                #region 表头校验
                var drTitle = importType==ImportType.AgencyCompany? _dataTable.Rows[1] : _dataTable.Rows[0];             
                for (int i = 0; i < _headerList.Count; i++)
                {
                    if (drTitle[i].ToString().Trim() != _headerList[i])
                    {
                        result.ErrorList.Add(new ImportDetailError() { ErrorMessage = $"导入数据出错, 模板表头第{i + 1}列应为{_headerList[i]}" });
                    }
                }
                #endregion

                #region 验证数据是否为空,大于0，自定义规则，添加数据
                DataRow dr;
                if (importType == ImportType.AgencyCompany)
                {
                     _dataTable.Rows[0].Delete();
                }
                for (int i = 1; i < _dataTable.Rows.Count; i++)
                {             
                    dr = _dataTable.Rows[i];
                    var excelDetailErrror = new ImportDetailError() { LineNo = i + 1 };
                    for (int j = 0; j < dr.ItemArray.Length; j++)
                    {
                        #region 非空判断
                        if (drTitle[j].ToString()!="备注"&&DBNull.Value == dr[j])
                        {
                            excelDetailErrror.ErrorMessage = $"【" + drTitle[j] + "为空】";
                        };
                        #endregion
                        #region 大于0
                        if (_greaterThen0List.Any(p => p == drTitle[j].ToString().Trim()))
                        {
                            var headerName = _greaterThen0List.FirstOrDefault(p => p == drTitle[j].ToString().Trim());
                            if (decimal.TryParse(dr[j].ToString(), out decimal value))
                            {
                                if (value < 0)
                                {
                                    excelDetailErrror.ErrorMessage += $"【{headerName}为负】";
                                }
                            }
                            else
                            {
                                excelDetailErrror.ErrorMessage += $"【{headerName}格式错误】";
                            }
                        }
                        #endregion
                    }
                    #region 自定义规则
                    if (_dataRowRule != null)
                    {
                        excelDetailErrror.ErrorMessage += _dataRowRule(dr);
                    }
                    #endregion
                    if (!string.IsNullOrEmpty(excelDetailErrror.ErrorMessage))
                    {
                        result.ErrorList.Add(excelDetailErrror);
                    }
                    #region _formatDataTable里添加数据
                    if (result.ErrorList.Count == 0)
                    {
                        if (_eachRowAction != null)
                        {
                            _eachRowAction(_formatDataTable, dr);
                        }
                    }
                    #endregion
                    #region 错误数量达到50就跳出循环
                    else if (result.ErrorList.Count >= 50)
                    {
                        break;
                    }
                    #endregion
                }
                #endregion
                result.FormatedDataTable = _formatDataTable;
                return result;
            });
        }
        #endregion
    }
}
