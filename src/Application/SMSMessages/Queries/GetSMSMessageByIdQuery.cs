using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Application.SMSMessages.Queries.Search;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.SMSMessages.Queries
{
    public class GetSMSMessageByIdQuery : IRequest<SMSMessageDto>
    {
        public int Id { get; set; }
    }

    public class GetSMSMessageQueryHandler : IRequestHandler<GetSMSMessageByIdQuery, SMSMessageDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSMSMessageQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SMSMessageDto> Handle(GetSMSMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var SMSMessage = await _context.SMSMessage
                        .ProjectTo<SMSMessageDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return SMSMessage;
        }
    }
}
