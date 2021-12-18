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

namespace ElarabyCA.Application.Employees.Queries.Search
{
    public class SearchEmployeesQuery : SearchQuery, IRequest<SearchEmployeesViewModel>
    {

    }

    public class SearchEmployeesQueryHandler : IRequestHandler<SearchEmployeesQuery, SearchEmployeesViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchEmployeesViewModel> Handle(SearchEmployeesQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchEmployeesViewModel();

            IQueryable<Employee> query = _context.Employee;
            result.TotalRecords = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Sort))
                query = query.OrderBy(request.Sort);

            if (request.PageIndex.HasValue && request.PageLength.HasValue)
            {
                query = query.Skip(request.PageIndex.Value * request.PageLength.Value)
                            .Take(request.PageLength.Value);
            }

            result.Data = await query
                        .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            return result;
        }
    }
}
