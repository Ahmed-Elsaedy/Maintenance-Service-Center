namespace ElarabyCore2.MiddleTier.DataTable
{
    public class DataTableColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public DataTableSearch Search { get; }
        public bool Orderable { get; set; }
    }
}