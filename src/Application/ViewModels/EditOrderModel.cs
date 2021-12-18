using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElarabyCore2.MiddleTier.ViewModels
{
    public class EditOrderModel
    {
        [Display(Name = "Order.OID")]
        public int Oid { get; set; }

        [Display(Name = "Order.ORDERID")]
        public string ORDERID { get; set; }

        [Display(Name = "Order.DateAssigned")]
        public DateTime DateAssigned { get; set; }

        [Required(ErrorMessage = "Attributes.Required")]
        [Display(Name = "Order.Customer")]
        public string Customer { get; set; }

        [Required(ErrorMessage = "Attributes.Required")]
        [Display(Name = "Order.PrimaryPhone")]
        public string Phone1 { get; set; }

        [Display(Name = "Order.SecondaryPhone")]
        public string Phone2 { get; set; }

        [Required(ErrorMessage = "Attributes.Required")]
        [Display(Name = "Order.Product")]
        public string Product { get; set; }

        [Required(ErrorMessage = "Attributes.Required")]
        [Display(Name = "Order.Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Attributes.Required")]
        [Display(Name = "Order.Complaint")]
        public string Complaint { get; set; }

        [Required(ErrorMessage = "Attributes.Required")]
        [Display(Name = "Order.Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Attributes.Required")]
        [Display(Name = "Order.Region")]
        public string Region { get; set; }

        [Display(Name = "Order.Notes")]
        public string Notes { get; set; }
    }

}
