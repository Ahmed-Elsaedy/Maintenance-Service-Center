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

namespace ElarabyCA.Application.FinancialTransactions.Commands
{
    public class CreateFinancialTransactionCommand : IRequest<int>
    {
        public int Type { get; set; }
        public int Amount { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public DateTime Date { get; set; }
        public int? EmployeeId { get; set; }
    }

    public class CreateFinancialTransactionCommandHandler : IRequestHandler<CreateFinancialTransactionCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public CreateFinancialTransactionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<int> Handle(CreateFinancialTransactionCommand request, CancellationToken cancellationToken)
        {
            FinancialTransaction entity = new FinancialTransaction()
            {
                Date = request.Date,
                Amount = request.Amount,
                Type = request.Type,
                Title = request.Title,
                Remarks = request.Remarks,
                EmployeeId = request.EmployeeId,
                Created = DateTime.Now,
                CreatedBy = _currentUserService.UserId,
                IsDeleted = false
            };
            await _context.FinancialTransaction.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.TransactionId;
        }
    }
}
