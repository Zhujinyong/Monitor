using Centa.Monitor.Common.Import.Interface;
using Centa.Monitor.Common.Import.Config;
using Centa.Monitor.Common.Interface;
using System;
using System.Data;
using static Centa.Monitor.Common.CommonHelper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Centa.Monitor.Common.Import
{
    /// <summary>
    /// 导入excel
    /// </summary>
    public class ImportExcel
    {
        #region private

        /// <summary>
        /// 文件
        /// </summary>
        private IFormFile _file;

        /// <summary>
        /// 数据
        /// </summary>
        private DataTable _dataTable;

        /// <summary>
        /// 导入类型
        /// </summary>
        private ImportType _importType;

        #endregion

        #region With

        public ImportExcel WithImportType(ImportType importType)
        {
            _importType = importType;
            return this;
        }

        public ImportExcel WithFile(IFormFile file)
        {
            _file = file;
            return this;
        }
        
        #endregion

        public IResult<ImportResult> DoImport()
        {
            var importResult = new ImportResult();
            return Execute<ImportResult>(() =>
            {
                #region 获取excel的DataTable
                var dataTableResult = LogExcuteTime<IResult<DataTable>>(this, () => {
                    return GetExcelDataTable(_file);
                }, "获取excel的DataTable");
                if (dataTableResult.Status != 200)
                {
                    throw new Exception(dataTableResult.Message);
                }
                else
                {
                    _dataTable = dataTableResult.Data;
                }
                #endregion

                #region 数据源校验
                #region iportItem赋值
                IImportItemConfig iportItem;
                switch (_importType)
                {
                    case ImportType.BasicCooperation:
                        iportItem = new BasicCooperationConfig();
                        break;
                    case ImportType.ResultCooperation:
                        iportItem = new ResultCooperationConfig();
                        break;
                    case ImportType.HumanOtherAddition:
                        iportItem = new HumanOtherAdditionConfig();
                        break;
                    case ImportType.HumanOtherMinus:
                        iportItem = new HumanOtherMinusConfig();
                        break;
                    case ImportType.FinanceOtherAddition:
                        iportItem = new FinanceOtherAdditionConfig();
                        break;
                    case ImportType.FinanceOtherMinus:
                        iportItem = new FinanceOtherMinusConfig();
                        break;
                    case ImportType.Surplus:
                        iportItem = new SurplusConfig();
                        break;
                    case ImportType.AgencyCompany:
                        iportItem = new AgencyCompanyConfig();
                        break;
                    default:
                        iportItem = new BasicCooperationConfig();
                        break;
                }
                #endregion
                var result = LogExcuteTime<IResult<ImportResult>>(this, () => {
                    return new ImportItemConfig().Import(iportItem, _dataTable,_importType);
                }, $"解析dataTable");


                if (result.Status == 200)
                {
                    importResult = result.Data;
                }
                else
                {
                    importResult.ErrorList = new List<ImportDetailError>() {
                            new ImportDetailError() { ErrorMessage=result.Message} };
                }
                #endregion

                return importResult;
            });
        }
    }
}
