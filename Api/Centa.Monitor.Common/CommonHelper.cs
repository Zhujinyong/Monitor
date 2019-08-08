using Centa.Monitor.Common.Interface;
using Centa.Monitor.Common.Log4net;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Centa.Monitor.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// 执行方法，统一捕获异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">要执行的方法</param>
        /// <returns>返回IResult<T></returns>
        public static IResult<T> Execute<T>(Func<T> func)
        {
            IResult<T> result = null;
            if (func != null)
            {
                try
                {
                    result = new Result<T>(data: func());
                }
                catch (Exception ex)
                {
                    result = new Result<T>(801, ex.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据表达式计算结果
        /// </summary>
        /// <param name="item">数据源</param>
        /// <param name="expression">表达式，用大括号包含，如：工号{工号}、实发{工资-社保-公积金}</param>
        /// <returns></returns>
        public static ExcelItem GetValue(dynamic item, string expression)
        {
            var result = new ExcelItem();
            result.IsContainsOperator = expression.IndexOf("+") > -1;
            var frontStr = "{";
            var endStr = result.IsContainsOperator ? "+" : "}";
            var expressionStr = GetMiddleString(expression, frontStr, endStr);

            var itemAdDict = ((IDictionary<String, Object>)item);
            object value;
            if (itemAdDict.TryGetValue(expressionStr, out value))
            {
                result.Value = value?.ToString();
            }
            return result;
        }

        public static ExcelItem GetValue2(dynamic item, string expression)
        {
            var result = new ExcelItem();
            var frontStr = "{";
            var endStr = "}";
            if (expression.Contains(frontStr) && expression.Contains(endStr))
            {
                //字符串表达式，如工资，工资-社保-公积金
                var expressionStr = GetMiddleString(expression, frontStr, endStr);

                //计算表达式
                var caculateExpression = expressionStr;

                //按属性长度倒序排序的属性集合，满足表达式的最长匹配
                var orderedPropertyList = ((IDictionary<String, Object>)item).OrderByDescending(p => p.Key.Length).ToList();

                //用数据替换属性，得到表达式
                foreach (var property in orderedPropertyList)
                {
                    caculateExpression = caculateExpression.Replace(property.Key, property.Value?.ToString());
                }

                //运算符集合
                List<string> operatorList = new List<string>() { "+", "-", "*", "/", "%" };

                //判断表达式是否包含运算符
                if (operatorList.Any(p => expressionStr.Contains(p)))
                {
                    //包含运算符
                    //计算表达式
                    result.Value = Eval(caculateExpression);
                    result.IsContainsOperator = true;
                }
                else
                {
                    //不包含运算符
                    result.Value = caculateExpression;
                    result.IsContainsOperator = false;
                }
            }
            else
            {
                result = item.expression;
            }
            return result;
        }

        public static string GetHeaderString(string expressionString)
        {
            var result = expressionString;
            var frontStr = "{";
            var endStr = "}";
            if (expressionString.Contains(frontStr) && expressionString.Contains(endStr))
            {
                result = expressionString.Substring(0, expressionString.IndexOf(frontStr));
            }
            return result;
        }

        /// <summary>
        /// 前后截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="front">前缀字符串</param>
        /// <param name="end">后缀字符串</param>
        /// <returns></returns>
        public static string GetMiddleString(string str, string front, string end)
        {
            var result = string.Empty;
            if (str.Contains(front))
            {
                var behindStr = str.Substring(str.IndexOf(front) + front.Length);
                if (behindStr.Contains(end))
                {
                    result = behindStr.Substring(0, behindStr.IndexOf(end));
                }
            }
            return result;
        }

        /// <summary>
        /// 计算表达式结果，如：2+1.1*6
        /// </summary>
        /// <param name="expressionStr"></param>
        /// <returns></returns>
        private static string Eval(string expressionStr)
        {
            var result = expressionStr;
            var computeResult = Execute<string>(() =>
            {
                return new DataTable().Compute(expressionStr, "").ToString();
            });
            if (computeResult.Status == 200)
            {
                result = computeResult.Data;
            }
            return result;
        }

        private static string GetValueString(Type type, object value)
        {
            var result = string.Empty;
            switch (type.ToString())
            {
                case "System.Decimal":
                    result = value.ToString();
                    break;
                case "System.String":
                    result = $"'{value.ToString()}'";
                    break;
            }
            return result;
        }

        public static string ToInsertUDTSql(DataTable dataTable, string varName)
        {
            StringBuilder sql = new StringBuilder($"DECLARE {varName} dbo.{dataTable.TableName} ");
            List<string> columns = new List<string>();
            foreach (DataColumn column in dataTable.Columns)
            {
                columns.Add(column.ColumnName);
            }
            int count = 2;
            for (int i = 0; i < count; i++)
            {
                if (i % 1000 == 0)
                {
                    sql.Append($"; INSERT INTO {varName} ({String.Join(",", columns)}) Values ");
                }
                sql.Append($"(");
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {

                    sql.Append(GetValueString(dataTable.Columns[j].DataType, dataTable.Rows[i][j]));
                    if (j < dataTable.Columns.Count - 1)
                    {
                        sql.Append($",");
                    }
                }
                sql.Append(")" + ((i + 1) % 1000 == 0 || i == count - 1 ? ";" : ","));
            }

            return sql.ToString();
        }

        public static IResult<Stream> GetStream(string url)
        {
            return Execute<Stream>(() =>
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response.GetResponseStream();
            });
        }

        public static T LogExcuteTime<T>(object obj, Func<T> func, string description)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            T result = default(T);
            if (func != null)
            {
                result = func();
            }
            stopWatch.Stop();
            Log.WriteLog(obj, $"{description},耗时：{stopWatch.ElapsedMilliseconds}ms");
            return result;
        }

        public static void LogExcuteTime(object obj, Action action, string description)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (action != null)
            {
                action();
            }
            stopWatch.Stop();
            Log.WriteLog(obj, $"{description},耗时：{stopWatch.ElapsedMilliseconds}ms");
        }



        public static IResult<DataTable> GetExcelDataTable(IFormFile file)
        {
            var dt = new DataTable();
            ISheet sheet = null;
            IWorkbook workbook = null;
            return Execute<DataTable>(() =>
            {
                var fileType = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                using (MemoryStream stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (fileType == "xlsx")
                    {
                        workbook = new XSSFWorkbook(stream);
                    }
                    else if (fileType == "xls")
                    {
                        workbook = new HSSFWorkbook(stream);
                    }
                    else
                    {
                        throw new Exception("文件后缀格式错误");
                    }
                }
                sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {

                    #region 设置列
                    IRow firstRow = sheet.GetRow(0);
                    if (firstRow != null)
                    {
                        for (int j = 0; j < firstRow.LastCellNum; j++)
                        {
                            dt.Columns.Add(j.ToString());
                        }
                    }
                    #endregion

                    #region 数据填充
                    IRow row;
                    DataRow drNew;
                    ICell cell;
                    for (int i = 0; i <= sheet.LastRowNum; i++)
                    {
                        row = sheet.GetRow(i);
                        #region drNew
                        if (row != null)
                        {
                            drNew = dt.NewRow();
                            for (int j = row.FirstCellNum; j < row.LastCellNum; ++j)
                            {
                                cell = row.GetCell(j);
                                if (cell != null)
                                {
                                    if (cell.CellType==CellType.Numeric)
                                    {
                                        short format = cell.CellStyle.DataFormat;
                                        if (format==14||format==31||format==57||format==58||format==177)
                                        {
                                            drNew[j] = cell.DateCellValue;//日期格式处理
                                        }
                                        else
                                        {
                                            drNew[j] = cell.NumericCellValue;
                                        }
                                    }
                                    else
                                    {
                                        drNew[j] = cell.ToString();
                                    }
                                    
                                }
                            }
                            dt.Rows.Add(drNew);
                        }
                        #endregion


                    }
                    #endregion
                    return dt;
                }
                return dt;
            });
        }

    }
}

