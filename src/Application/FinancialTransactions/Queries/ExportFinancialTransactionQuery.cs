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

namespace ElarabyCA.Application.Transactions.Queries.Search
{
    public class ExportFinancialTransactionQuery : IRequest<ExportFinancialTransactionQueryViewModel>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public bool Withdrawal { get; set; }
        public bool Deposit { get; set; }
    }

    public class ExportFinancialTransactionQueryViewModel
    {
        public List<SearchFinancialTransactionDto> Data { get; set; }
    }

    public class ExportFinancialTransactionQueryHandler : IRequestHandler<ExportFinancialTransactionQuery, ExportFinancialTransactionQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportFinancialTransactionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExportFinancialTransactionQueryViewModel> Handle(ExportFinancialTransactionQuery request, CancellationToken cancellationToken)
        {
            var result = new ExportFinancialTransactionQueryViewModel();

            IQueryable<FinancialTransaction> query = _context.FinancialTransaction;

            query = query.Where(x => x.IsDeleted == null || x.IsDeleted == false);

            if (!request.Deposit || !request.Withdrawal)
            {
                if (request.Deposit)
                    query = query.Where(x => x.Type == 7);

                if (request.Withdrawal)
                    query = query.Where(x => x.Type == 8);
            }

            if (request.FromDate.HasValue)
                query = query.Where(x => x.Date >= request.FromDate);

            if (request.ToDate.HasValue)
                query = query.Where(x => x.Date <= request.ToDate);

            result.Data = await query
                      .ProjectTo<SearchFinancialTransactionDto>(_mapper.ConfigurationProvider)
                      .ToListAsync(cancellationToken);

            return result;
        }
    }
}
