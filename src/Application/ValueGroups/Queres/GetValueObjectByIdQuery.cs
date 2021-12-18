using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.ValueGroups.Queres
{
    public class GetValueGroupByIdQuery : IRequest<ValueGroupDto>
    {
        public int Id { get; set; }
    }

    public class GetValueGroupByIdQueryHandler : IRequestHandler<GetValueGroupByIdQuery, ValueGroupDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetValueGroupByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ValueGroupDto> Handle(GetValueGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.ValueGroup
                        .ProjectTo<ValueGroupDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.ValueGroupId == request.Id, cancellationToken);

            return result;
        }
    }
}