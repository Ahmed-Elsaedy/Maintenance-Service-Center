using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Inventories.Commands
{
    public class UpdateInventoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInventoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Inventory.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Inventory), request.Id);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
