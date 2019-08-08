using Centa.Monitor.Common.Import.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Centa.Monitor.Common.Import.Config
{
    public class AgencyCompanyConfig : IImportItemConfig
    {
        public string DataTableName { get; set; } = "";

        #region HeaderList

        public List<string> HeaderList { get; set; } = new List<string>() {
                          "序号",
                          "开票日期",
                          "合伙人姓名",
                          "合伙人身份证号码",
                          "购买方",
                          "发票类型",
                          "开票金额",
                          "金额（不含税）",
                          "增值税税额",
                          "税率",
                          "附加税",
                          "税率",
                          "个人所得税",
                          "税率"};
        #endregion

        #region GreaterThen0List
        public List<string> GreaterThen0List { get; set; } = new List<string>() {
                          "金额(不含税)",
                          "增值税税额",
                          "附加税",
                          "个人所得税"};
        #endregion

        #region Rule
        public Func<DataRow, string> Rule { get; set; } = (dr) =>
        {
            var message = string.Empty;
            if (dr[5].ToString().Trim() != "专用发票" && dr[5].ToString().Trim() != "普通发票")
            {
                message += "【发票类型错误,应为专用/普通发票】";
            }
            return message;
        };
        #endregion

        #region TableTypeColumns
        public Dictionary<string, Type> TableTypeColumns { get; set; } = new Dictionary<string, Type>() {
            { "EmployeeName", typeof(string)},
            { "IDCard", typeof(string)},
            { "MicroEnterpriseName", typeof(string)},
            { "InvoiceDate", typeof(DateTime)},
            { "GeneralIncomeTax", typeof(decimal)},
            { "GeneralVAT", typeof(decimal)},
            { "GeneralInvoice", typeof(decimal)},
            { "GeneralSurtax", typeof(decimal)},
            { "ProfessionalIncomeTax", typeof(decimal)},
            { "ProfessionalVAT", typeof(decimal)},
            { "ProfessionalInvoice", typeof(decimal)},
            { "ProfessionalSurtax", typeof(decimal)}
        };

        #endregion

        #region EachRowAction
        public Action<DataTable, DataRow> EachRowAction { get; set; } = (dt, dr) =>
        {
            var isOrdinaryInvoice = dr[5].ToString() == "普通发票";
            dt.Rows.Add(
                dr[2].ToString().Trim(),
                dr[3].ToString().Trim(),
                dr[4].ToString().Trim(),
                DateTime.Parse(dr[1].ToString().Trim()),
                isOrdinaryInvoice ? decimal.Parse(dr[12].ToString()) : 0,//GeneralIncomeTax
                isOrdinaryInvoice ? decimal.Parse(dr[8].ToString()) : 0,//GeneralVAT
                isOrdinaryInvoice ? decimal.Parse(dr[6].ToString()) : 0,//GeneralInvoice
                isOrdinaryInvoice ? decimal.Parse(dr[10].ToString()) : 0,//GeneralSurtax
                !isOrdinaryInvoice ? decimal.Parse(dr[12].ToString()) : 0,//ProfessionalIncomeTax
                !isOrdinaryInvoice ? decimal.Parse(dr[8].ToString()) : 0,//ProfessionalVAT
                !isOrdinaryInvoice ? decimal.Parse(dr[6].ToString()) : 0,//ProfessionalInvoice
                !isOrdinaryInvoice ? decimal.Parse(dr[10].ToString()) : 0//ProfessionalSurtax
                );
        };
        #endregion
    }
}
