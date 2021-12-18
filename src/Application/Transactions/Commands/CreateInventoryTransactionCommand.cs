using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Helpers;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using ElarabyCA.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.InventoryTransactions.Commands
{
    public class CreateInventoryTransactionCommand : IRequest<int>
    {
        public int InventoryId { get; set; }

        public int? Type { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }

        public int? StoreId { get; set; }

        public int? ReferenceType { get; set; }
        public string ReferenceId { get; set; }
    }

    public class CreateInventoryTransactionCommandHandler : IRequestHandler<CreateInventoryTransactionCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateInventoryTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateInventoryTransactionCommand request, CancellationToken cancellationToken)
        {
            InventoryTransaction entity = new InventoryTransaction()
            {
                Amount = request.Amount,
                ReferenceId = request.ReferenceId,
                ReferenceType = request.ReferenceType,
                Description = request.Description,
                Type = request.Type,
                InventoryId = request.InventoryId
            };

            var inventory = await _context.Inventory
                    .FirstOrDefaultAsync(x => x.InventoryId == request.InventoryId, cancellationToken);

            var transactionTypeValue = await _context.ValueGroup
                    .FirstOrDefaultAsync(x => x.ValueGroupId == entity.Type.Value, cancellationToken);

            if (Enum.TryParse(typeof(TransactionType), transactionTypeValue.Value, out object transactionType))
            {
                switch ((TransactionType)transactionType)
                {
                    case TransactionType.Addition:
                        inventory.CurrentBalance += request.Amount.Value;
                        _context.InventoryTransaction.Add(entity);
                        break;
                    case TransactionType.Substraction:
                        if ((inventory.CurrentBalance - request.Amount.Value) < 0)
                            throw new ForbiddenException(Messages.InventoryTransaction.AmountIsGreateThanCurrentBalance);
                        inventory.CurrentBalance -= request.Amount.Value;
                        entity.Amount = entity.Amount * -1;
                        _context.InventoryTransaction.Add(entity);
                        break;
                    case TransactionType.Transfer:
                        var toInventory = await _context.Inventory
                            .FirstOrDefaultAsync(x => x.StoreId == request.StoreId && x.SparePartId == inventory.SparePartId, cancellationToken);

                        if ((inventory.CurrentBalance - request.Amount.Value) < 0)
                            throw new ForbiddenException(Messages.InventoryTransaction.AmountIsGreateThanCurrentBalance);

                        inventory.CurrentBalance -= request.Amount.Value;
                        entity.Amount = entity.Amount * -1;
                        inventory.InventoryTransactions.Add(entity);

                        if (toInventory == null)
                        {
                            toInventory = new Inventory()
                            {
                                StoreId = request.StoreId.Value,
                                SparePartId = inventory.SparePartId,
                            };
                            _context.Inventory.Add(toInventory);
                        }
                        toInventory.CurrentBalance += request.Amount.Value;
                        toInventory.InventoryTransactions.Add(
                            new InventoryTransaction()
                                {
                                    Amount = request.Amount,
                                    ReferenceId = request.ReferenceId,
                                    ReferenceType = 1002,
                                    Description = request.Description,
                                    Type = request.Type,
                                });

                        _context.InventoryTransaction.Add(entity);
                        break;
                }
            }

            
            await _context.SaveChangesAsync(cancellationToken);

            return entity.TransactionId;
        }
    }
}
