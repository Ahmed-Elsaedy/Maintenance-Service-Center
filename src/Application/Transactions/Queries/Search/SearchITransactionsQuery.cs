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
    public class SearchITransactionsQuery : SearchQuery, IRequest<SearchITransactionsQueryViewModel>
    {
        public int? InventoryId { get; set; }
    }

    public class SearchITransactionsQueryHandler : IRequestHandler<SearchITransactionsQuery, SearchITransactionsQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchITransactionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchITransactionsQueryViewModel> Handle(SearchITransactionsQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchITransactionsQueryViewModel();

            IQueryable<InventoryTransaction> query = _context.InventoryTransaction;

            if (request.InventoryId.HasValue)
                query = query.Where(x => x.InventoryId == request.InventoryId);

            result.TotalRecords = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Sort))
                query = query.OrderBy(request.Sort);

            if (request.PageIndex.HasValue && request.PageLength.HasValue)
            {
                query = query.Skip(request.PageIndex.Value * request.PageLength.Value)
                            .Take(request.PageLength.Value);
            }

            result.Data = await query
                        .ProjectTo<SearchITransactionDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            var refTypes = await _context.ValueGroup.Where(x => x.Group == "ReferenceType")
                        .ToListAsync(cancellationToken);
            foreach (var d in result.Data)
                if (d.ReferenceType.HasValue)
                    d.ReferenceTypeValue = refTypes.SingleOrDefault(x => x.ValueGroupId == d.ReferenceType.Value)?.Value;

            return result;
        }
    }
}
