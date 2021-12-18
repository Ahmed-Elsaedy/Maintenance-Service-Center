using ElarabyCore2.DataLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ElarabyCA.Domain.Entities
{
    public partial class Employee : IDbModel
    {
        public Employee()
        {
            OrderTicket = new HashSet<OrderTicket>();
            FinancialTransactions = new HashSet<FinancialTransaction>();
            Stores = new HashSet<Store>();
        }

        public int Oid { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public bool? ChangePasswordOnFirstLogon { get; set; }
        public string StoredPassword { get; set; }
        public int? OptimisticLockField { get; set; }
        public int? Gcrecord { get; set; }
        public int? ObjectType { get; set; }
        public bool? IsTechnician { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<FinancialTransaction> FinancialTransactions { get; set; }
        public virtual ICollection<OrderTicket> OrderTicket { get; set; }
    }
}
