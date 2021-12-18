using ElarabyCA.Domain.Common;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class ValueGroup : AuditableEntity
    {
        public ValueGroup()
        {
            InventoryTransactionTypes = new HashSet<InventoryTransaction>();
            FinancialTransactionTypes = new HashSet<FinancialTransaction>();
            SparePartCategories = new HashSet<SparePart>();
        }

        public int ValueGroupId { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<InventoryTransaction> InventoryTransactionTypes { get; set; }
        public virtual ICollection<FinancialTransaction> FinancialTransactionTypes { get; set; }
        public virtual ICollection<SparePart> SparePartCategories { get; set; }
    }
}
