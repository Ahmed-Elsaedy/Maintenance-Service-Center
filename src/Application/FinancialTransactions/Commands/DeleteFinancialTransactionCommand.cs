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

namespace ElarabyCA.Application.FinancialTransactions.Commands
{
    public class DeleteFinancialTransactionCommand : IRequest
    {
        public int TransactionId { get; set; }
    }

    public class DeleteFinancialTransactionCommandHandler : IRequestHandler<DeleteFinancialTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFinancialTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFinancialTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialTransaction.Where(l => l.TransactionId == request.TransactionId)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(FinancialTransaction), request.TransactionId);

            entity.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
