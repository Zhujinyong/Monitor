using Centa.Monitor.Common.Import.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Common.Import.Config
{
    /// <summary>
    /// 财务其他减项
    /// </summary>
    public class FinanceOtherMinusConfig : IImportItemConfig
    {
        public string DataTableName { get; set; } = "";

        #region HeaderList
        public List<string> HeaderList { get; set; } = new List<string>() {
                          "员工编号",
                          "员工姓名",
                          "财务减项1",
                          "财务减项2",
                          "其他减项合计",
                          "备注"};
        #endregion

        #region GreaterThen0List
        public List<string> GreaterThen0List { get; set; } = new List<string>() {
                          "财务减项1",
                          "财务减项2",
                          "其他减项合计"};
        #endregion

        #region Rule
        public Func<DataRow, string> Rule { get; set; } = (dr) =>
              {
                  var message = string.Empty;
                  decimal financeMinus1;
                  decimal.TryParse(dr[2].ToString().Trim(), out financeMinus1);
                  decimal financeMinus2;
                  decimal.TryParse(dr[3].ToString().Trim(), out financeMinus2);
                  decimal fOMFee;
                  decimal.TryParse(dr[4].ToString().Trim(), out fOMFee);
                  if (financeMinus1 + financeMinus2 != fOMFee)
                  {
                      message += "【其他减项合计错误,请重新计算】";
                  }
                  if (dr[5] != DBNull.Value && dr[5].ToString().Trim().Length > 32)
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
            { "FinanceMinus1", typeof(decimal)},
            { "FinanceMinus2", typeof(decimal)},
            { "FOMFee", typeof(decimal)},
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
                decimal.Parse(dr[3].ToString().Trim()),
                decimal.Parse(dr[4].ToString().Trim()),
                dr[5].ToString().Trim()
                );
        };
        #endregion

    }
}
