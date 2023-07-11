using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Base;
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
using System.Linq.Dynamic.Core;
using ElarabyCA.Application.Stores.Queries;
using ElarabyCA.Domain.Enums;

namespace ElarabyCA.Application.Transactions.Queries.Search
{
    public class FinancialTransactionsStatisticsQuery : IRequest<FinancialTransactionsStatisticsQueryViewModel>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class FinancialTransactionsStatisticsQueryViewModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int TotalIncomes { get; set; }
        public int TotalExpenses { get; set; }
        public int Savings { get; set; }
    }

    public class FinancialTransactionsStatisticsQueryHandler : IRequestHandler<FinancialTransactionsStatisticsQuery, FinancialTransactionsStatisticsQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FinancialTransactionsStatisticsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FinancialTransactionsStatisticsQueryViewModel> Handle(FinancialTransactionsStatisticsQuery request, CancellationToken cancellationToken)
        {
            var transactionTypes = await _context.ValueGroup
                .Where(x => x.Group == nameof(FinancialTransactionType))
                .ToListAsync();

            var deposit = transactionTypes.FirstOrDefault(x => x.Value == nameof(FinancialTransactionType.Income));
            var widthrawal = transactionTypes.FirstOrDefault(x => x.Value == nameof(FinancialTransactionType.Expense));

            var result = new FinancialTransactionsStatisticsQueryViewModel();

            IQueryable<FinancialTransaction> query = _context.FinancialTransaction;

            query = query.Where(x => x.IsDeleted == null || x.IsDeleted == false);

            if (request.FromDate.HasValue)
                query = query.Where(x => x.Date >= request.FromDate);

            if (request.ToDate.HasValue)
                query = query.Where(x => x.Date <= request.ToDate);

            result.FromDate = request.FromDate;
            result.ToDate = result.ToDate;
            result.TotalIncomes = await query.Where(x => x.Type == deposit.ValueGroupId).SumAsync(x => x.Amount.Value);
            result.TotalExpenses = await query.Where(x => x.Type == widthrawal.ValueGroupId).SumAsync(x => x.Amount.Value);
            result.Savings = result.TotalIncomes - result.TotalExpenses;

            return result;
        }
    }
}
