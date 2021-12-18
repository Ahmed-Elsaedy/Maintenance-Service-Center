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

namespace ElarabyCA.Application.Transactions.Queries.Search
{
    public class SearchFinancialTransactionsQuery : SearchQuery, IRequest<SearchFinancialTransactionsQueryViewModel>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class SearchFinancialTransactionsQueryHandler : IRequestHandler<SearchFinancialTransactionsQuery, SearchFinancialTransactionsQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchFinancialTransactionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchFinancialTransactionsQueryViewModel> Handle(SearchFinancialTransactionsQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchFinancialTransactionsQueryViewModel();

            IQueryable<FinancialTransaction> query = _context.FinancialTransaction.Include(x => x.TypeNavigation);

            query = query.Where(x => x.IsDeleted == null || x.IsDeleted == false);

            if (request.FromDate.HasValue)
                query = query.Where(x => x.Date >= request.FromDate);

            if (request.ToDate.HasValue)
                query = query.Where(x => x.Date <= request.ToDate);

            result.TotalRecords = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Sort))
                query = query.OrderBy(request.Sort);

            if (request.PageIndex.HasValue && request.PageLength.HasValue)
            {
                query = query.Skip(request.PageIndex.Value * request.PageLength.Value)
                            .Take(request.PageLength.Value);
            }

            result.Data = await query
                        .ProjectTo<SearchFinancialTransactionDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            var refTypes = await _context.ValueGroup.Where(x => x.Group == "FinancialTransactionType")
                        .ToListAsync(cancellationToken);

            return result;
        }
    }
}
