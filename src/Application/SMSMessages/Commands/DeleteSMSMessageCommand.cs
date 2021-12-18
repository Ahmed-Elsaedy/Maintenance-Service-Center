using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Helpers;
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

namespace ElarabyCA.Application.SMSMessages.Commands
{
    public class DeleteSMSMessageCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteSMSMessageCommandHandler : IRequestHandler<DeleteSMSMessageCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSMSMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSMSMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SMSMessage.Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(SMSMessage), request.Id);

            if (_context.OrderSMSMessage.Any(x => x.SMSMessageId == entity.Id))
                throw new ForbiddenException(Messages.SMSMessage.CannotDeleteSMSMessageHasOrder);

            _context.SMSMessage.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
