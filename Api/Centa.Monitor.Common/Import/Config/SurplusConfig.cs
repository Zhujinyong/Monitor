using Centa.Monitor.Common.Import.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Common.Import.Config
{
    /// <summary>
    /// 导入转小微前结余
    /// </summary>
    public class SurplusConfig : IImportItemConfig
    {
        public string DataTableName { get; set; } = "";

        #region HeaderList
        public List<string> HeaderList { get; set; } = new List<string>() {
                          "员工编号",
                          "员工姓名",
                          "结余金额",
                          "备注"};
        #endregion

        #region GreaterThen0List
        public List<string> GreaterThen0List { get; set; } = new List<string>() {
                          "结余金额"};
        #endregion

        #region Rule
        public Func<DataRow, string> Rule { get; set; } = (dr) =>
              {
                  var message = string.Empty;
                  if (dr[3] != DBNull.Value && dr[3].ToString().Trim().Length > 32)
                  {
                      message += "【备注超出最大长度】";
                  }
                  return message;
              };
        #endregion

        #region TableTypeColumns
        public Dictionary<string, Type> TableTypeColumns { get; set; } = new Dictionary<string, Type>() {
            { "EmployeeNo", typeof(string)},
            { "EmployeeName", typeof(string)},
            { "Surplus", typeof(decimal)},
            { "Remark", typeof(string)}
        };

        #endregion

        #region EachRowAction
        public Action<DataTable, DataRow> EachRowAction { get; set; } = (dt, dr) =>
        {
            dt.Rows.Add(
                dr[0].ToString().Trim(),
                dr[1].ToString().Trim(),
                decimal.Parse(dr[2].ToString().Trim()),
                dr[3].ToString().Trim()
                );
        };
        #endregion

    }
}
