using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Helpers;
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
    public class DeleteOrderSMSMessageCommand : IRequest
    {
        public List<int> Ids { get; set; }
    }

    public class DeleteOrderSMSMessageCommandHandler : IRequestHandler<DeleteOrderSMSMessageCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOrderSMSMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOrderSMSMessageCommand request, CancellationToken cancellationToken)
        {
            var entities = _context.OrderSMSMessage.Where(x => request.Ids.Contains(x.Id));

            //if (entities.Count() == 0)
            //    throw new NotFoundException(nameof(OrderSMSMessage), request.Ids);

            _context.OrderSMSMessage.RemoveRange(entities);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
