using Centa.Monitor.Common.Interface;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using static Centa.Monitor.Common.CommonHelper;


namespace Centa.Monitor.Common
{
    /// <summary>
    /// 下载文件操作类
    /// </summary>
   public static class Download
    {
        /// <summary>        
        /// 下载文件        
        /// </summary>        
        /// <param name="URL">下载文件地址</param>       
        /// <param name="Filename">下载后的存放地址</param>        
        public static void DownloadFile(string URL, string filename)
        {
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(URL);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                Stream st = myrp.GetResponseStream();
                Stream so = new FileStream(filename, FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
            }
            catch
            {
                throw;
            }
        }


        public static DataSet GetDataSet(string path)
        {
            var streamResult = GetStream(path);
            var dataSetResult = Download.GetDataSet(streamResult.Data, path.Substring(path.LastIndexOf(".") + 1));
            return dataSetResult.Data;
        }

        /// <summary>
        /// Excel转换成DataSet（.xlsx/.xls）
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static IResult<DataSet> GetDataSet(Stream fs,string fileType)
        {
           return Execute<DataSet>(()=> {
                DataSet ds = new DataSet();
                ISheet sheet = null;
                int sheetNumber = 0;
                IWorkbook workbook = null;
                if (fileType == "xlsx")
                {
                    LogExcuteTime(typeof(int), () => {
                       workbook = new XSSFWorkbook(fs);
                   }, $"XSSFWorkbook");

                   
                }
                else if (fileType == "xls")
                {
                    workbook = new HSSFWorkbook(fs);
                }
               fs.Close();
               sheetNumber = workbook.NumberOfSheets;
                for (int i = 0; i < sheetNumber; i++)
                {
                    string sheetName = workbook.GetSheetName(i);
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet != null)
                    {
                       var dataTableResult = LogExcuteTime<IResult<DataTable>>(typeof(DataTable), () => {
                           return GetSheetDataTable(sheet);
                       }, $"GetSheetDataTable");

                       //var dataTableResult = GetSheetDataTable(sheet);
                        if (dataTableResult.Status == 200)
                        {
                            dataTableResult.Data.TableName = sheetName.Trim();
                            ds.Tables.Add(dataTableResult.Data);
                        }
                        else
                        {
                            throw new Exception("Sheet数据获取失败");
                        }
                    }
                }
                return ds;
            });
        }

        /// <summary>
        /// 获取sheet表对应的DataTable
        /// </summary>
        /// <param name="sheet">Excel工作表</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        private static IResult<DataTable> GetSheetDataTable(ISheet sheet)
        {
            return Execute<DataTable>(()=> {
                var dt = new DataTable();
                #region 设置列
                IRow firstRow = sheet.GetRow(0);
                if (firstRow!=null)
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
                                drNew[j] = cell.ToString();
                                
                            }
                        }
                        dt.Rows.Add(drNew);
                    }
                    #endregion
                    
                   
                }
                #endregion
                return dt;
            });
        }

        /// <summary>
        /// 去掉excel的空白行
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable removeEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool rowdataisnull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {

                        rowdataisnull = false;
                    }
                }
                if (rowdataisnull)
                {
                    removelist.Add(dt.Rows[i]);
                }

            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
            return dt;
        }



    }
}
