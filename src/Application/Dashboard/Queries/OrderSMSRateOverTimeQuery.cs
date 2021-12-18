using AutoMapper;
using ElarabyCA.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Dashboard.Queries
{
    public class OrderSMSRateOverTimeQuery : IRequest<OrderSMSRateOverTimeViewModel>
    {
        public bool IsMonths { get; set; }
        public int Count { get; set; }
    }

    public class OrderSMSRateOverTimeViewModel
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }

    public class OrderSMSRateOverTimeHandler : IRequestHandler<OrderSMSRateOverTimeQuery, OrderSMSRateOverTimeViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderSMSRateOverTimeHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderSMSRateOverTimeViewModel> Handle(OrderSMSRateOverTimeQuery request, CancellationToken cancellationToken)
        {
            var result = new OrderSMSRateOverTimeViewModel();
            result.Data = new List<int>();
            result.Labels = new List<string>();

            if (request.IsMonths)
            {
                for (int i = 0; i < request.Count; i++)
                {
                    var targetMonth = DateTime.Now.AddMonths(i == 0 ? 0 : -i);
                    var startDate = new DateTime(targetMonth.Year, targetMonth.Month, 1);
                    var endDate = startDate.AddMonths(1);

                    var count = _context.OrderSMSMessage.Where(x => x.LastSent > startDate && x.LastSent < endDate).Count();
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

                    var count = _context.OrderSMSMessage.Where(x => x.LastSent > startDate && x.LastSent < endDate).Count();
                    result.Labels.Insert(0, $"{startDate.Month}/{startDate.Day}");
                    result.Data.Insert(0, count);
                }
            }

            return await Task.FromResult(result);
        }
    }
}
