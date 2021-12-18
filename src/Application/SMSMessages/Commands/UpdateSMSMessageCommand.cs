using ElarabyCA.Application.Common.Exceptions;
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
    public class UpdateSMSMessageCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class UpdateSMSMessageCommandHandler : IRequestHandler<UpdateSMSMessageCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSMSMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSMSMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SMSMessage.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(SMSMessage), request.Id);

            entity.Title = request.Title;
            entity.Text = request.Text;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
