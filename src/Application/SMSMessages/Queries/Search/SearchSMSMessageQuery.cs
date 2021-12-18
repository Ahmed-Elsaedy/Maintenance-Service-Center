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

namespace ElarabyCA.Application.SMSMessages.Queries.Search
{
    public class SearchSMSMessagesQuery : SearchQuery, IRequest<SearchSMSMessagesViewModel>
    {

    }

    public class SearchSMSMessagesQueryHandler : IRequestHandler<SearchSMSMessagesQuery, SearchSMSMessagesViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchSMSMessagesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchSMSMessagesViewModel> Handle(SearchSMSMessagesQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchSMSMessagesViewModel();

            IQueryable<SMSMessage> query = _context.SMSMessage;
            result.TotalRecords = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Sort))
                query = query.OrderBy(request.Sort);

            if (request.PageIndex.HasValue && request.PageLength.HasValue)
            {
                query = query.Skip(request.PageIndex.Value * request.PageLength.Value)
                            .Take(request.PageLength.Value);
            }

            result.Data = await query
                        .ProjectTo<SearchSMSMessageDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            return result;
        }
    }
}
