using ElarabyCA.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Domain.Entities
{
    public partial class FinancialTransaction : AuditableEntity
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public int? Type { get; set; }
        public int? Amount { get; set; }
        public string Title { get; set; }
        public virtual ValueGroup TypeNavigation { get; set; }
        public string Remarks { get; set; }
        public bool? IsDeleted { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee EmployeeNavigation { get; set; }
    }
}
