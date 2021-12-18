using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Inventories.Commands
{
    public class CreateInventoryCommand : IRequest<int>
    {

    }

    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateInventoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            Inventory entity = new Inventory();

            _context.Inventory.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.InventoryId;
        }
    }
}
