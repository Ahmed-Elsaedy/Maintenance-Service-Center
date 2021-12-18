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


namespace ElarabyCA.Application.Dashboard.Queries
{
    public class TicketsByEmployeesQuery : IRequest<TicketsByEmployeesQueryViewModel>
    {
        public int Months { get; set; }
        public int Days { get; set; }
    }

    public class TicketsByEmployeesQueryViewModel
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }

    public class TicketsByEmployeesQueryHandler : IRequestHandler<TicketsByEmployeesQuery, TicketsByEmployeesQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TicketsByEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TicketsByEmployeesQueryViewModel> Handle(TicketsByEmployeesQuery request, CancellationToken cancellationToken)
        {
            var result = new TicketsByEmployeesQueryViewModel();
            result.Data = new List<int>();
            result.Labels = new List<string>();

            var groupResult = _context.OrderTicket.Where(x => x.IsActive == true)
                    .Where(x => x.Date > DateTime.Now.AddMonths(request.Months).AddDays(request.Days))
                    .GroupBy(f => f.EmployeeNavigation.DisplayName)
                    .Select(g => new { Employee = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count);
                    //.Take(10);

            //var categoryIds = groupResult.Select(x => x.CategoryId).ToList();
            //var categories = _context.TicketCategory.Where(x => categoryIds.Contains(x.Oid));

            //groupResult.ToList().ForEach(g =>
            //{
            //    var cat = categories.SingleOrDefault(x => x.Oid == g.CategoryId);
            //    result.Labels.Add(cat == null ? "" : cat.Title);
            //    result.Data.Add(g.Count);
            //});

            groupResult.ToList().Select(g =>
            {
                return new { Label = g.Employee, Data = g.Count };
            }).OrderByDescending(x => x.Data).ToList().ForEach(g => { result.Labels.Add(g.Label); result.Data.Add(g.Data); });


            return await Task.FromResult(result);
        }
    }
}
