using ElarabyCA.Application.Common.Exceptions;
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

namespace ElarabyCA.Application.Employees.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Employee.Where(l => l.Oid == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Employee), request.Id);

            _context.Employee.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
