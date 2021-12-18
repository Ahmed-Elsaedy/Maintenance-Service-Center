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

namespace ElarabyCA.Application.Inventories.Commands
{
    public class DeleteInventoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteInventoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Inventory.Where(l => l.InventoryId == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Inventory), request.Id);

            _context.Inventory.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
