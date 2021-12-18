using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Application.Employees.Commands;
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
    public class UpdateOrderPhonesCommand : IRequest<int>
    {
        public int OrderId { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
    }

    public class UpdateOrderPhonesCommandHandler : IRequestHandler<UpdateOrderPhonesCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public UpdateOrderPhonesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateOrderPhonesCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Order.FirstOrDefaultAsync(x => x.Oid == request.OrderId, cancellationToken);

            if (order == null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            order.PrimaryPhone = request.PrimaryPhone;
            order.SecondaryPhone = request.SecondaryPhone;

            await _context.SaveChangesAsync(cancellationToken);

            return order.Oid;
        }
    }
}
