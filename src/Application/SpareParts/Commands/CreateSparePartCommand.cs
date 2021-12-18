using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace ElarabyCA.Application.SpareParts.Commands
{
    public class CreateSparePartCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int StoreId { get; set; }
        public int OpeningBalance { get; set; }
    }

    public class CreateSparePartCommandHandler : IRequestHandler<CreateSparePartCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateSparePartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateSparePartCommand request, CancellationToken cancellationToken)
        {
            SparePart entity = new SparePart()
            {
                Title = request.Title,
                Barcode = request.Barcode,
                Description = request.Description
            };

            Inventory inventory = new Inventory()
            {
                SparePart = entity,
                StoreId = request.StoreId,
                OpeningBalance = request.OpeningBalance,
                CurrentBalance = request.OpeningBalance,
            };

            _context.SparePart.Add(entity);
            _context.Inventory.Add(inventory);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.SparePartId;
        }
    }
}
