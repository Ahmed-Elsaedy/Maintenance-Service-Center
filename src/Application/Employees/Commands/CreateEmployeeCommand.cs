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
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Administrator { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee entity = new Employee();

            _context.Employee.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Oid;
        }
    }
}
