using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Base;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace ElarabyCA.Application.Inventories.Queries.Search
{
    public class SearchInventoriesQuery : SearchQuery, IRequest<SearchInventoriesQueryViewModel>
    {
        public int? StoreId { get; set; }
        public int? SparePartId { get; set; }
    }

    public class SearchInventoriesQueryHandler : IRequestHandler<SearchInventoriesQuery, SearchInventoriesQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchInventoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchInventoriesQueryViewModel> Handle(SearchInventoriesQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchInventoriesQueryViewModel();

            IQueryable<Inventory> query = _context.Inventory;

            if (request.StoreId.HasValue)
                query = query.Where(x => x.StoreId == request.StoreId.Value);

            if (request.SparePartId.HasValue)
                query = query.Where(x => x.SparePartId == request.SparePartId.Value);

            result.TotalRecords = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Sort))
                query = query.OrderBy(request.Sort);

            if (request.PageIndex.HasValue && request.PageLength.HasValue)
            {
                query = query.Skip(request.PageIndex.Value * request.PageLength.Value)
                            .Take(request.PageLength.Value);
            }

            result.Data = await query
                        .ProjectTo<SearchInventoryDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            return result;
        }
    }
}
