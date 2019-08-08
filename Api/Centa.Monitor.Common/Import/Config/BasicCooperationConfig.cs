using Centa.Monitor.Common.Import.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Common.Import.Config
{
    /// <summary>
    /// 基础合作费
    /// </summary>
    public class BasicCooperationConfig : IImportItemConfig
    {
        #region DataTableName
        public string DataTableName { get; set; }= "hbcTableType";
        #endregion

        #region HeaderList
        public List<string> HeaderList { get; set; } = new List<string>() {
                          "员工编号",
                          "员工姓名",
                          "底薪",
                          "社保",
                          "公积金",
                          "奖金",
                          "其他底薪加项",
                          "基础合作费",
                          "备注"};
        #endregion

        #region GreaterThen0List
        public List<string> GreaterThen0List { get; set; } = new List<string>() {
                          "底薪",
                          "社保",
                          "公积金",
                          "奖金",
                          "其他底薪加项",
                          "基础合作费"};
        #endregion

        #region Rule
        public Func<DataRow, string> Rule { get; set; } = (dr) =>
              {
                  var message = string.Empty;
                  decimal salary;
                  decimal.TryParse(dr[2].ToString().Trim(), out salary);
                  decimal socialInsurance;
                  decimal.TryParse(dr[3].ToString().Trim(), out socialInsurance);
                  decimal housingFund;
                  decimal.TryParse(dr[4].ToString().Trim(), out housingFund);
                  decimal bonus;
                  decimal.TryParse(dr[5].ToString().Trim(), out bonus);
                  decimal otherSalaryAddition;
                  decimal.TryParse(dr[6].ToString().Trim(), out otherSalaryAddition);
                  decimal bCFee;
                  decimal.TryParse(dr[7].ToString().Trim(), out bCFee);
                  if (salary + socialInsurance + housingFund + bonus + otherSalaryAddition != bCFee)
                  {
                      message += "【基础合作费错误,请重新计算】";
                  }
                  if (dr[8] != DBNull.Value && dr[8].ToString().Trim().Length > 32)
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
            { "Salary", typeof(decimal)},
            { "SocialInsurance", typeof(decimal)},
            { "HousingFund", typeof(decimal)},
            { "Bonus", typeof(decimal)},
            { "OtherSalaryAddition", typeof(decimal)},
            { "BCFee", typeof(decimal)},
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
                decimal.Parse(dr[6].ToString().Trim()),
                decimal.Parse(dr[7].ToString().Trim()),
                dr[8].ToString().Trim()
                );
        };

        
        #endregion

    }
}
