using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Application.Transactions.Queries.Search;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.FinancialTransactions.Queries
{
    public class GetFinancialTransactionById : IRequest<SearchFinancialTransactionDto>
    {
        public int Id { get; set; }
    }

    public class GetFinancialTransactionQueryHandler : IRequestHandler<GetFinancialTransactionById, SearchFinancialTransactionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFinancialTransactionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchFinancialTransactionDto> Handle(GetFinancialTransactionById request, CancellationToken cancellationToken)
        {
            var FinancialTransaction = await _context.FinancialTransaction
                        .Include(x=> x.EmployeeNavigation)
                        .Include(x=> x.TypeNavigation)
                        .ProjectTo<SearchFinancialTransactionDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.TransactionId == request.Id, cancellationToken);

            return FinancialTransaction;
        }
    }
}
