using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.ValueGroups.Queres
{
    public class GetValueGroupsByGroupQuery : IRequest<IList<ValueGroupDto>>
    {
        public string Group { get; set; }
    }

    public class GetValueGroupsByGroupQueryHandler : IRequestHandler<GetValueGroupsByGroupQuery, IList<ValueGroupDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetValueGroupsByGroupQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<ValueGroupDto>> Handle(GetValueGroupsByGroupQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.ValueGroup
                        .ProjectTo<ValueGroupDto>(_mapper.ConfigurationProvider)
                        .Where(x => x.Group == request.Group)
                        .ToListAsync(cancellationToken);

            return result;
        }
    }
}