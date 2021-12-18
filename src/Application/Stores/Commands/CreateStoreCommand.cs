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
    public class CreateStoreCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Administrator { get; set; }
    }

    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            Store entity = new Store();
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Administrator = request.Administrator;

            _context.Store.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.StoreId;
        }
    }
}
