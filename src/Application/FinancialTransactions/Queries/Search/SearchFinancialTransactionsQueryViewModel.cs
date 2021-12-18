using ElarabyCA.Application.Common.Base;
using ElarabyCA.Application.Stores.Queries;
using ElarabyCA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.Transactions.Queries.Search
{
    public class SearchFinancialTransactionsQueryViewModel : SearchQueryViewModel<SearchFinancialTransactionDto>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
