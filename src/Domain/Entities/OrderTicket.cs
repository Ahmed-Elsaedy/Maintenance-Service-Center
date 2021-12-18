using ElarabyCore2.DataLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class OrderTicket : IDbModel
    {
        public OrderTicket()
        {
            OrderNavigation = new HashSet<Order>();
        }

        public int Oid { get; set; }
        public string UserName { get; set; }
        public DateTime? Date { get; set; }
        public string Report { get; set; }
        public int? Order { get; set; }
        public int? Employee { get; set; }
        public int? Category { get; set; }
        public bool? IsActive { get; set; }
        public int? OptimisticLockField { get; set; }
        public int? Gcrecord { get; set; }
        public bool? CauseUnCompletion { get; set; }
        public bool? Notable { get; set; }
        public bool? IncludeInOrderNotes { get; set; }

        public virtual TicketCategory CategoryNavigation { get; set; }
        public virtual Employee EmployeeNavigation { get; set; }
        public virtual Order Order1 { get; set; }
        public virtual ICollection<Order> OrderNavigation { get; set; }
    }
}
