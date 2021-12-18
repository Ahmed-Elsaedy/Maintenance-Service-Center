using System;
using System.ComponentModel.DataAnnotations;

namespace ElarabyCore2.MiddleTier.ViewModels
{
    public class TicketModel
    {
        [Display(Name = "OrderTicket.OID")]
        public int? OID { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "OrderTicket.Date")]
        public DateTime Date { get; set; }
        [Display(Name = "OrderTicket.Report")]
        public string Report { get; set; }
        [Display(Name = "OrderTicket.Employee")]
        public string Employee { get; set; }
        [Display(Name = "OrderTicket.Category")]
        public string Category { get; set; }
        public bool IsActive { get; set; }

        public int OrderOid { get; set; }
        [Display(Name = "OrderTicket.Category")]
        public int CategoryId { get; set; }
        [Display(Name = "OrderTicket.Employee")]
        public int EmployeeId { get; set; }

        public bool? IsEmployeeActive { get; set; }
    }
}
