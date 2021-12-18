using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ElarabyCA.Application.SpareParts.Commands
{
    public class UpdateSparePartCommand : IRequest
    {
        public int SparePartId { get; set; }
        public string Barcode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateSparePartCommandHandler : IRequestHandler<UpdateSparePartCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSparePartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSparePartCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SparePart.FindAsync(request.SparePartId);

            if (entity == null)
                throw new NotFoundException(nameof(SparePart), request.SparePartId);

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Barcode = request.Barcode;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
