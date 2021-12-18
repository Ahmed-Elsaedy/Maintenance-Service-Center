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

namespace ElarabyCA.Application.OrderSMSMessages.Queries.Search
{
    public class SearchOrderSMSMessagesQuery : SearchQuery, IRequest<SearchOrderSMSMessagesQueryViewModel>
    {
        public int? OrderId { get; set; }
        public int? SMSMessageId { get; set; }
    }

    public class SearchOrderSMSMessagesQueryHandler : IRequestHandler<SearchOrderSMSMessagesQuery, SearchOrderSMSMessagesQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchOrderSMSMessagesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchOrderSMSMessagesQueryViewModel> Handle(SearchOrderSMSMessagesQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchOrderSMSMessagesQueryViewModel();

            IQueryable<OrderSMSMessage> query = _context.OrderSMSMessage;

            if (request.OrderId.HasValue)
                query = query.Where(x => x.OrderId == request.OrderId.Value);

            if (request.SMSMessageId.HasValue)
                query = query.Where(x => x.SMSMessageId == request.SMSMessageId.Value);

            result.TotalRecords = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Sort))
                query = query.OrderBy(request.Sort);

            if (request.PageIndex.HasValue && request.PageLength.HasValue)
            {
                query = query.Skip(request.PageIndex.Value * request.PageLength.Value)
                            .Take(request.PageLength.Value);
            }

            result.Data = await query
                        .ProjectTo<SearchOrderSMSMessageDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            return result;
        }
    }
}
