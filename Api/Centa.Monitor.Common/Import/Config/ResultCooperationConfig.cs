using Centa.Monitor.Common.Import.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Common.Import.Config
{
    /// <summary>
    /// 成果合作费
    /// </summary>
    public class ResultCooperationConfig : IImportItemConfig
    {
        public string DataTableName { get; set; } = "";

        #region HeaderList
        public List<string> HeaderList { get; set; } = new List<string>() {
                          "员工编号",
                          "员工姓名",
                          "佣金",
                          "绩效奖金",
                          "其他佣金加项",
                          "成果合作费",
                          "备注"};
        #endregion

        #region GreaterThen0List
        public List<string> GreaterThen0List { get; set; } = new List<string>() {
                          "佣金",
                          "绩效奖金",
                          "其他佣金加项",
                          "成果合作费"};
        #endregion

        #region Rule
        public Func<DataRow, string> Rule { get; set; } = (dr) =>
              {
                  var message = string.Empty;
                  decimal commission;
                  decimal.TryParse(dr[2].ToString().Trim(), out commission);
                  decimal performance;
                  decimal.TryParse(dr[3].ToString().Trim(), out performance);
                  decimal otherCommissionAddition;
                  decimal.TryParse(dr[4].ToString().Trim(), out otherCommissionAddition);
                  decimal fRCFee;
                  decimal.TryParse(dr[5].ToString().Trim(), out fRCFee);
                  if (commission + performance + otherCommissionAddition  != fRCFee)
                  {
                      message += "【成果合作费错误,请重新计算】";
                  }
                  if (dr[6] != DBNull.Value && dr[6].ToString().Trim().Length > 32)
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
            { "Commission", typeof(decimal)},
            { "Performance", typeof(decimal)},
            { "OtherCommissionAddition", typeof(decimal)},
            { "FRCFee", typeof(decimal)},
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
                decimal.Parse(dr[5].ToString().Trim()),
                dr[6].ToString().Trim()
                );
        };
        #endregion

    }
}
