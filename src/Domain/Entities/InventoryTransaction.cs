using ElarabyCA.Domain.Common;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class InventoryTransaction : AuditableEntity
    {
        public int TransactionId { get; set; }
        public int InventoryId { get; set; }

        public int? Type { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }

        public int? ReferenceType { get; set; }
        public string ReferenceId { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual ValueGroup TypeNavigation { get; set; }
    }
}
