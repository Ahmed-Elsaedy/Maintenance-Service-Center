using System.Collections.Generic;

namespace ElarabyCore2.MiddleTier.DataTable
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DataTableSearch Search { get; set; }
        public List<DataTableColumn> Columns { get; set; }
        public List<DataTableOrder> Order { get; set; }
        public IDictionary<string, object> AdditionalParameters { get; set; }
    }
}