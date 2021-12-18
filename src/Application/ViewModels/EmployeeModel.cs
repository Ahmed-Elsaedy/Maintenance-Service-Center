using ElarabyCore2.MiddleTier.Annotations;

namespace ElarabyCore2.MiddleTier.ViewModels
{
    public class EmployeeModel
    {
        [IndexViewField(Title = "OID", Sortable = true, SortOrder = 1,
            SortDirection = FieldSortDirection.Descending)]
        public int Oid { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        public string PrimaryPhone { get; set; }

        public string SecondaryPhone { get; set; }
    }
}
