using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Application.SpareParts.Queries;
using ElarabyCA.Application.SpareParts.Queries.Search;
using ElarabyCA.Application.Stores.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Stores.Queries
{
    public class GetSparePartById : IRequest<SparePartDto>
    {
        public int Id { get; set; }
    }

    public class GetSparePartQueryHandler : IRequestHandler<GetSparePartById, SparePartDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSparePartQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SparePartDto> Handle(GetSparePartById request, CancellationToken cancellationToken)
        {
            var sparePart = await _context.SparePart
                        .ProjectTo<SparePartDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.SparePartId == request.Id, cancellationToken);

            return sparePart;
        }
    }
}
