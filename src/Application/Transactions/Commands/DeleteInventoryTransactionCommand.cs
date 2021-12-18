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

namespace ElarabyCA.Application.InventoryTransactions.Commands
{
    public class DeleteInventoryTransactionCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteInventoryTransactionCommandHandler : IRequestHandler<DeleteInventoryTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteInventoryTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInventoryTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.InventoryTransaction.Where(l => l.TransactionId == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(InventoryTransaction), request.Id);

            _context.InventoryTransaction.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
