using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Application.Stores.Queries.Search;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Stores.Queries
{
    public class GetStoreById : IRequest<SearchStoreDto>
    {
        public int Id { get; set; }
    }

    public class GetStoreQueryHandler : IRequestHandler<GetStoreById, SearchStoreDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoreQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchStoreDto> Handle(GetStoreById request, CancellationToken cancellationToken)
        {
            var store = await _context.Store
                        .Include("AdministratorNavigation")
                        .ProjectTo<SearchStoreDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.StoreId == request.Id, cancellationToken);

            return store;
        }
    }
}
