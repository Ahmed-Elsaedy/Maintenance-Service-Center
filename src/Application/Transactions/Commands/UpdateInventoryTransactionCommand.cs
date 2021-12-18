using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.InventoryTransactions.Commands
{
    public class UpdateInventoryTransactionCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class UpdateInventoryTransactionCommandHandler : IRequestHandler<UpdateInventoryTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInventoryTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInventoryTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.InventoryTransaction.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(InventoryTransaction), request.Id);


            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
