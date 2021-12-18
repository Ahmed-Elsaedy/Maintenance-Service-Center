using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.SMSMessages.Commands
{
    public class CreateSMSMessageCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class CreateSMSMessageCommandHandler : IRequestHandler<CreateSMSMessageCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateSMSMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateSMSMessageCommand request, CancellationToken cancellationToken)
        {
            SMSMessage entity = new SMSMessage();
            entity.Text = request.Text;
            entity.Title = request.Title;

            _context.SMSMessage.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
