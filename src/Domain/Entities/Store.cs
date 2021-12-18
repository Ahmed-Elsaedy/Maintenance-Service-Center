using ElarabyCA.Domain.Common;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class Store : AuditableEntity
    {
        public Store()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int StoreId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Administrator { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Employee AdministratorNavigation { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
