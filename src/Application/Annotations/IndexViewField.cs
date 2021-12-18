using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCore2.MiddleTier.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class IndexViewField : Attribute
    {
        public string Title { get; set; }
        public bool Sortable { get; set; }

        public int SortOrder { get; set; }
        public FieldSortDirection SortDirection { get; set; }

        public IndexViewField()
        {

        }
    }

    public enum FieldSortDirection
    {
        Ascending,
        Descending
    }
}
