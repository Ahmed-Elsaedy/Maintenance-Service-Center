using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.FinancialTransactions.Commands
{
    public class UpdateFinancialTransactionCommand : IRequest
    {
        public int TransactionId { get; set; }
        public int Type { get; set; }
        public int Amount { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
    }

    public class UpdateFinancialTransactionCommandHandler : IRequestHandler<UpdateFinancialTransactionCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateFinancialTransactionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateFinancialTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialTransaction.FindAsync(request.TransactionId);

            if (entity == null)
                throw new NotFoundException(nameof(FinancialTransaction), request.TransactionId);

            entity.Title = request.Title;
            entity.Date = request.Date;
            entity.Amount = request.Amount;
            entity.Remarks = request.Remarks;
            entity.Type = request.Type;
            entity.EmployeeId = request.EmployeeId;
            entity.LastModified = DateTime.Now;
            entity.LastModifiedBy = _currentUserService.UserId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
