using ElarabyCA.Domain.Common;
using System;

namespace ElarabyCA.Domain.Entities
{
    public class OrderSMSMessage : AuditableEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int SMSMessageId { get; set; }
        public virtual SMSMessage SMSMessage { get; set; }
        public DateTime? LastSent { get; set; }
        public int SendCount { get; set; }
        public string Report { get; set; }
        public string Phone { get; set; }
    }
}
