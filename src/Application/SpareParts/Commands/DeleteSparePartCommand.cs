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

namespace ElarabyCA.Application.SpareParts.Commands
{
    public class DeleteSparePartCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteSparePartCommandHandler : IRequestHandler<DeleteSparePartCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSparePartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSparePartCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SparePart.Where(l => l.SparePartId == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(SparePart), request.Id);

            var inventory = _context.Inventory.FirstOrDefault(x => x.SparePartId == entity.SparePartId);

            if (inventory != null)
            {
                if (_context.InventoryTransaction.Any(x => x.InventoryId == inventory.InventoryId))
                    throw new ForbiddenException(Messages.SparePart.CannotDeleteSparePartHasInventory);
                else
                    _context.Inventory.Remove(inventory);
            }
            
            _context.SparePart.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
