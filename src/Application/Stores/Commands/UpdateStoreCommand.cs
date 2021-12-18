using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Stores.Commands
{
    public class UpdateStoreCommand : IRequest
    {
        public int StoreId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Administrator { get; set; }
    }

    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Store.FindAsync(new object[] { request.StoreId}, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Store), request.StoreId);

            entity.Title = request.Title;
            entity.Administrator = request.Administrator;
            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
