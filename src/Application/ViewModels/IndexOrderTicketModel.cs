using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCore2.MiddleTier.ViewModels
{
    public class IndexOrderTicketModel
    {
        public int Oid { get; set; }
        public int? Order { get; set; }
        public string UserName { get; set; }
        public DateTime? Date { get; set; }
        public string Report { get; set; }
        public int Employee { get; set; }
        public int Category { get; set; }
        public string EmployeeName { get; set; }
        public object CategoryTitle { get; internal set; }
    }
}
