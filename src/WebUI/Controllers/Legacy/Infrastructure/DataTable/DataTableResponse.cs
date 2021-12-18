using System.Collections.Generic;

namespace ElarabyCore2.MiddleTier.DataTable
{
    public class DataTableResponse
    {
        public int Draw { get; set; }

        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }

        public object Data { get; set; }

        public string Error { get; set; }

        public IDictionary<string, object> AdditionalParameters { get; set; }
    }
}