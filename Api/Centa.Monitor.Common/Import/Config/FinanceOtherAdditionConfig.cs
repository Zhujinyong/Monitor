using Centa.Monitor.Common.Import.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Common.Import.Config
{
    /// <summary>
    /// 财务其他加项
    /// </summary>
    public class FinanceOtherAdditionConfig : IImportItemConfig
    {
        public string DataTableName { get; set; } = "";

        #region HeaderList
        public List<string> HeaderList { get; set; } = new List<string>() {
                          "员工编号",
                          "员工姓名",
                          "财务加项1",
                          "财务加项2",
                          "其他加项合计",
                          "备注"};
        #endregion

        #region GreaterThen0List
        public List<string> GreaterThen0List { get; set; } = new List<string>() {
                          "财务加项1",
                          "财务加项2",
                          "其他加项合计"};
        #endregion

        #region Rule
        public Func<DataRow, string> Rule { get; set; } = (dr) =>
              {
                  var message = string.Empty;
                  decimal financeAddition1;
                  decimal.TryParse(dr[2].ToString().Trim(), out financeAddition1);
                  decimal financeAddition2;
                  decimal.TryParse(dr[3].ToString().Trim(), out financeAddition2);
                  decimal fOAFee;
                  decimal.TryParse(dr[4].ToString().Trim(), out fOAFee);
                  if (financeAddition1 + financeAddition2 != fOAFee)
                  {
                      message += "【其他加项合计错误,请重新计算】";
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
            { "FinanceAddition1", typeof(decimal)},
            { "FinanceAddition2", typeof(decimal)},
            { "FOAFee", typeof(decimal)},
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
