using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;
using ElarabyCore2.MiddleTier.Annotations;
using System;
using System.Collections.Generic;

namespace ElarabyCore2.MiddleTier.ViewModels
{
    public class IndexOrderModel 
    {
        [IndexViewField(Title = "OID", Sortable = true, SortOrder = 1,
            SortDirection = FieldSortDirection.Descending)]
        public int Oid { get; set; }

        public string Orderid { get; set; }

        [IndexViewField(Title = "Date", Sortable = true)]
        public DateTime? DateAssigned { get; set; }

        public string Customer { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }

        public string Region { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }

        public string Product { get; set; }
        public string Model { get; set; }
        public string Complaint { get; set; }

        public int? ActiveTicket { get; set; }
        public string TicketCategory { get; set; }
        public string TicketReport { get; set; }
        public string TicketEmployee { get; set; }
 
    }
}


