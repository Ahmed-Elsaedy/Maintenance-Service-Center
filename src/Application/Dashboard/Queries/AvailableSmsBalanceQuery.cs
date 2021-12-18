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
    public class AvailableSmsBalanceQuery : IRequest<AvailableSmsBalanceViewModel>
    {
    }

    public class AvailableSmsBalanceViewModel
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }

    public class AvailableSmsBalanceHandler : IRequestHandler<AvailableSmsBalanceQuery, AvailableSmsBalanceViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AvailableSmsBalanceHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AvailableSmsBalanceViewModel> Handle(AvailableSmsBalanceQuery request, CancellationToken cancellationToken)
        {
            var result = new AvailableSmsBalanceViewModel();
            result.Data = new List<int>();
            result.Labels = new List<string>();

            int currentBalance, maxBalance;
            var currentBalanceValue = await _context.ValueGroup.FirstOrDefaultAsync(x => x.Group == "CurrentSMSBalance");
            currentBalance = int.Parse(currentBalanceValue.Value);
            var maxBalanceValue = await _context.ValueGroup.FirstOrDefaultAsync(x => x.Group == "MaxSMSBalance");
            maxBalance = int.Parse(maxBalanceValue.Value);

            result.Labels.Add("Available");
            result.Data.Add(maxBalance - currentBalance);

            result.Labels.Add("Used");
            result.Data.Add(currentBalance);

            return await Task.FromResult(result);
        }
    }
}
