using ElarabyCore2.DataLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class Order : IDbModel
    {
        public Order()
        {
            OrderTicket = new HashSet<OrderTicket>();
            OrderSMSMessages = new HashSet<OrderSMSMessage>();
        }

        public int Oid { get; set; }
        public string Orderid { get; set; }
        public string Customer { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string Product { get; set; }
        public string Model { get; set; }
        public string Complaint { get; set; }
        public DateTime? DateAssigned { get; set; }
        public int? InterfaceStatus { get; set; }
        public DateTime? DateLastDelayed { get; set; }
        public string ReasonLastDelayed { get; set; }
        public bool? IsSynchronized { get; set; }
        public int? OptimisticLockField { get; set; }
        public int? Gcrecord { get; set; }
        public string ReportsOverview { get; set; }
        public bool? IsArchived { get; set; }
        public bool? IsArgent { get; set; }
        public int? ActiveTicket { get; set; }
        public string OverviewNotes { get; set; }
        public string Tag { get; set; }
        public int? Priority { get; set; }
        public int? SiteIndex { get; set; }
        public int? RowBytesCount { get; set; }
        public Guid? WorkOrderId { get; set; }
        public string CaseNumber { get; set; }
        public int? Sapnumber { get; set; }

        public virtual OrderTicket ActiveTicketNavigation { get; set; }
        public virtual ICollection<OrderTicket> OrderTicket { get; set; }
        public ICollection<OrderSMSMessage> OrderSMSMessages { get; set; }
    }
}
