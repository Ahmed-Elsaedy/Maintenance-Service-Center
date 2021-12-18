using ElarabyCore2.DataLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class TicketCategory : IDbModel
    {
        public TicketCategory()
        {
            OrderTicket = new HashSet<OrderTicket>();
        }

        public int Oid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? BackColor { get; set; }
        public int? FontColor { get; set; }
        public int? FontStyle { get; set; }
        public bool? SupportApplyInBatches { get; set; }
        public bool? AllowDuplicates { get; set; }
        public int? OptimisticLockField { get; set; }
        public int? Gcrecord { get; set; }
        public bool? Reportable { get; set; }
        public int? Priority { get; set; }
        public bool? Activatable { get; set; }
        public bool? IncludeInOrderNotes { get; set; }

        public virtual ICollection<OrderTicket> OrderTicket { get; set; }
    }
}
