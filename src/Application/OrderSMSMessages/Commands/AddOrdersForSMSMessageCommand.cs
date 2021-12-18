using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.OrderSMSMessages.Commands
{
    public class AddOrdersForSMSMessageCommand : IRequest
    {
        public int SMSMessageId { get; set; }
        public string OrdersIds { get; set; }
    }

    public class AddOrdersForSMSMessageCommandHandler : IRequestHandler<AddOrdersForSMSMessageCommand>
    {
        private readonly IApplicationDbContext _context;

        public AddOrdersForSMSMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddOrdersForSMSMessageCommand request, CancellationToken cancellationToken)
        {
            var smsMessage = await _context.SMSMessage.FindAsync(new object[] { request.SMSMessageId }, cancellationToken);

            if (smsMessage == null)
                throw new NotFoundException(nameof(Domain.Entities.SMSMessage), request.SMSMessageId);

            var alreadyExistedOrdersIds = await _context.OrderSMSMessage
                    .Where(x => x.SMSMessageId == request.SMSMessageId)
                    .Select(x => x.OrderId).ToListAsync(cancellationToken);

            var newOrdersIds = request.OrdersIds.Split(';').Select(x => int.Parse(x))
                                    .Where(x => !alreadyExistedOrdersIds.Contains(x));

            var validOrders = await _context.Order.Where(x => newOrdersIds.Contains(x.Oid))
                                             .ToListAsync(cancellationToken);

            foreach (var order in validOrders)
            {
                OrderSMSMessage entity = new OrderSMSMessage()
                {
                    SMSMessageId = smsMessage.Id,
                    OrderId = order.Oid,
                    //Sent = false,
                    Report = ""
                };

                if (!string.IsNullOrEmpty(order.PrimaryPhone))
                    if (order.PrimaryPhone.StartsWith("01"))
                        order.PrimaryPhone = "2" + order.PrimaryPhone;
                    else if (order.PrimaryPhone.StartsWith("1"))
                        order.PrimaryPhone = "20" + order.PrimaryPhone;

                if (!string.IsNullOrEmpty(order.SecondaryPhone))
                    if (order.SecondaryPhone.StartsWith("01"))
                        order.SecondaryPhone = "2" + order.SecondaryPhone;
                    else if (order.SecondaryPhone.StartsWith("1"))
                        order.SecondaryPhone = "20" + order.SecondaryPhone;

                _context.OrderSMSMessage.Add(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}