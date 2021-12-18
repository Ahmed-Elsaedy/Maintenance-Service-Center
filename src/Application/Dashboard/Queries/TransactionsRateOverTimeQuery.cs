using AutoMapper;
using ElarabyCA.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Dashboard.Queries
{
    public class TransactionsRateOverTimeQuery : IRequest<TransactionsRateOverTimeViewModel>
    {
        public List<int> StoreIds { get; set; }
        public bool IsMonths { get; set; }
        public int Count { get; set; }
    }

    public class TransactionsRateOverTimeViewModel
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }

    public class TransactionsRateOverTimeHandler : IRequestHandler<TransactionsRateOverTimeQuery, TransactionsRateOverTimeViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransactionsRateOverTimeHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionsRateOverTimeViewModel> Handle(TransactionsRateOverTimeQuery request, CancellationToken cancellationToken)
        {
            var result = new TransactionsRateOverTimeViewModel();
            result.Data = new List<int>();
            result.Labels = new List<string>();

            if (request.IsMonths)
            {
                for (int i = 0; i < request.Count; i++)
                {
                    var targetMonth = DateTime.Now.AddMonths(i == 0 ? 0 : -i);
                    var startDate = new DateTime(targetMonth.Year, targetMonth.Month, 1);
                    var endDate = startDate.AddMonths(1);

                    var query = _context.InventoryTransaction.Where(x => x.Created > startDate && x.Created < endDate);
                    if (request.StoreIds != null && request.Count > 0)
                        query = query.Include("Inventory").Where(x => request.StoreIds.Contains(x.Inventory.StoreId));

                    var count = query.Count();
                    result.Labels.Insert(0, $"{startDate.Year}/{startDate.Month}");
                    result.Data.Insert(0, count);
                }
            }
            else
            {
                for (int i = 0; i < request.Count; i++)
                {
                    var startDate = DateTime.Now.AddDays(i == 0 ? 0 : -i).Date;
                    var endDate = startDate.AddDays(1);

                    var query = _context.InventoryTransaction.Where(x => x.Created > startDate && x.Created < endDate);
                    if (request.StoreIds != null && request.Count > 0)
                        query = query.Include("Inventory").Where(x => request.StoreIds.Contains(x.Inventory.StoreId));

                    var count = query.Count();
                    result.Labels.Insert(0, $"{startDate.Month}/{startDate.Day}");
                    result.Data.Insert(0, count);
                }
            }

            return await Task.FromResult(result);
        }
    }
}
