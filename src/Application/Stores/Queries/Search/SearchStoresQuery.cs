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

namespace ElarabyCA.Application.Stores.Queries.Search
{
    public class SearchStoresQuery : SearchQuery, IRequest<SearchStoresQueryViewModel>
    {

    }

    public class SearchStoresQueryHandler : IRequestHandler<SearchStoresQuery, SearchStoresQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchStoresQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchStoresQueryViewModel> Handle(SearchStoresQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchStoresQueryViewModel();

            IQueryable<Store> query = _context.Store;
            result.TotalRecords = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Sort))
                query = query.OrderBy(request.Sort);

            if (request.PageIndex.HasValue && request.PageLength.HasValue)
            {
                query = query.Skip(request.PageIndex.Value * request.PageLength.Value)
                            .Take(request.PageLength.Value);
            }

            result.Data = await query
                        .Include("AdministratorNavigation")
                        .ProjectTo<SearchStoreDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            return result;
        }
    }

}
