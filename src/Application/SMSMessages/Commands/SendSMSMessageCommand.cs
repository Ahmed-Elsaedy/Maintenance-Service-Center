using AutoMapper;
using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Application.OrderSMSMessages.Queries.Search;
using ElarabyCA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.SMSMessages.Commands
{
    public class SendSMSMessageCommand : IRequest<SearchOrderSMSMessageDto>
    {
        public int OrderSMSMessageId { get; set; }
        public bool UseSecondary { get; set; }
    }


    public class SendSMSMessageCommandHandler : IRequestHandler<SendSMSMessageCommand, SearchOrderSMSMessageDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SendSMSMessageCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SearchOrderSMSMessageDto> Handle(SendSMSMessageCommand command, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new SearchOrderSMSMessageDto() {  });
        }
    }
}
