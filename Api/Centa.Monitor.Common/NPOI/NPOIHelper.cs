using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using static Centa.Monitor.Common.CommonHelper;

namespace Centa.Monitor.Common.NPOI
{
    public static class NPOIHelper
    {
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据</param>
        /// <param name="excelTempPath">模板路径</param>
        /// <returns></returns>
        public static MemoryStream ExportExcel<T>(this IEnumerable<T> list ,string excelTempPath) 
        {
            if (!File.Exists(excelTempPath))
            {
                throw new Exception("模板文件不存在");
            }
            IWorkbook workbook = new XSSFWorkbook();
            using (FileStream fs = new FileStream(excelTempPath, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fs);
            }
            ISheet sheet = workbook.GetSheetAt(0);
            
            #region 单元格格式
            //货币格式
            var cellMoneyStyle = workbook.GetCellStyle(NPOICellStyle.Money);

            //四位小数个数
            var cell4DecimalStyle = workbook.GetCellStyle(NPOICellStyle.Decimal_Length_4);
            #endregion

            #region 遍历填充
            var header = sheet.GetRow(0);
            IRow row;
            ICell cell;
            var rowIndex = 1;
            ExcelItem excelItem;
            foreach (var item in list)
            {
                row = sheet.CreateRow(rowIndex);
                for (int i = 0; i < header.Cells.Count; i++)
                {
                    excelItem = GetValue(item.ToDynamic(), header.Cells[i].StringCellValue);
                    if (excelItem.IsContainsOperator)
                    {
                        cell = row.CreateCell(i, CellType.Numeric);
                        cell.SetCellValue(double.Parse(excelItem.Value));
                        cell.CellStyle = header.Cells[i].StringCellValue.Contains("比例") ? cell4DecimalStyle : cellMoneyStyle;
                    }
                    else if (!header.Cells[i].StringCellValue.Contains("FirstInvoiceDate")
                        &&(header.Cells[i].StringCellValue.Contains("InvoiceDate")
                           || header.Cells[i].StringCellValue.Contains("PaymentDate")))
                    {
                        //日期格式化
                        cell = row.CreateCell(i, CellType.String);
                        cell.SetCellValue(string.IsNullOrEmpty(excelItem.Value)
                            ? excelItem.Value : Convert.ToDateTime(excelItem.Value).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        row.CreateCell(i, CellType.String).SetCellValue(excelItem.Value);
                    }
                }
                rowIndex++;
            }
            #endregion

            #region 保留第一个sheet,修改header头
            //保留第一个sheet，其他的删除
            for (var i = 1; i < workbook.NumberOfSheets; i++)
            {
                workbook.RemoveSheetAt(1);
            }
            //修改header头
            for (int i = 0; i < header.Cells.Count; i++)
            {
                header.Cells[i].SetCellValue(GetHeaderString(header.Cells[i].StringCellValue));
            }
            #endregion

            var memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            memoryStream.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }

        public static ICellStyle GetCellStyle(this IWorkbook workbook, NPOICellStyle MonitorCellStyle)
        {
            var cellStyle = workbook.CreateCellStyle();
            switch (MonitorCellStyle)
            {
                case NPOICellStyle.Money:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00");
                    break;
                case NPOICellStyle.Decimal_Length_4:
                    cellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("0.0000");
                    break;
            }
            return cellStyle;
        }
    }
}
