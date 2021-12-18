using ElarabyCA.Application.Common.Exceptions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Administrator { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Employee.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Employee), request.Id);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
