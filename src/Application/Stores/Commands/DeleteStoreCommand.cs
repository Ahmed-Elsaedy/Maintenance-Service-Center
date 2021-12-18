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

namespace ElarabyCA.Application.Stores.Commands
{
    public class DeleteStoreCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Store.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Store), request.Id);

            if (_context.Inventory.Any(x => x.StoreId == entity.StoreId))
                throw new ForbiddenException(Messages.Store.CannotDeleteStoreHasInventory);


            _context.Store.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
