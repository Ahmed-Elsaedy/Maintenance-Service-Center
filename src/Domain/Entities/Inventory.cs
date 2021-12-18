using ElarabyCA.Domain.Common;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class Inventory : AuditableEntity
    {
        public Inventory()
        {
            InventoryTransactions = new HashSet<InventoryTransaction>();
        }

        public int InventoryId { get; set; }
        public int StoreId { get; set; }
        public int SparePartId { get; set; }

        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual SparePart SparePart { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    }
}
