using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Centa.Monitor.Common.Import
{
    public class ImportResult
    {
        public List<ImportDetailError> ErrorList { get; set; }

        public DataTable FormatedDataTable { get; set; }
    }
}
