using ElarabyCA.Domain.Common;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class SparePart : AuditableEntity
    {
        public SparePart()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int SparePartId { get; set; }
        public string Barcode { get; set; }
        public string Title { get; set; }
        public int? Category { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ValueGroup CategoryNavigation { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
