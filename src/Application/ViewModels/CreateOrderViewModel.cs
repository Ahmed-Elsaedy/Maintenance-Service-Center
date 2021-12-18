using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCore2.MiddleTier.ViewModels
{
    public class CreateOrderViewModel
    {
        public int? Oid { get; set; }
        public string Address { get; set; }
        public string Customer { get; set; }
        public string Complaint { get; set; }
        public DateTime? DateAssigned { get; set; }
        public string Model { get; set; }
        public string Street { get; set; }
        public string Orderid { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string Product { get; set; }
        public string Region { get; set; }
        public int? ActiveTicket { get; set; }

        public Guid? WorkOrderId { get; set; }
        public int? SAPNumber { get; set; }
        public string CaseNumber { get; set; }
    }
}
