using ElarabyCA.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Domain.Entities
{
    public class SMSMessage : AuditableEntity
    {
        public SMSMessage()
        {
            OrderSMSMessages = new HashSet<OrderSMSMessage>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public ICollection<OrderSMSMessage> OrderSMSMessages { get; set; }
    }
}
