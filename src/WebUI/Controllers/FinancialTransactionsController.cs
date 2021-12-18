using ElarabyCA.Application.FinancialTransactions.Commands;
using ElarabyCA.Application.FinancialTransactions.Queries;
using ElarabyCA.Application.Transactions.Queries.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    public class FinancialTransactionsController : ApiController
    {
        [HttpPost("Search")]
        public async Task<ActionResult<SearchFinancialTransactionsQueryViewModel>> Search(SearchFinancialTransactionsQuery query)
        {
            return await Mediator.Send(query ?? new SearchFinancialTransactionsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SearchFinancialTransactionDto>> GetById(int id)
        {
            return await Mediator.Send(new GetFinancialTransactionById() { Id = id });
        }

        [HttpPost("Statistics")]
        public async Task<ActionResult<FinancialTransactionsStatisticsQueryViewModel>> Statistics(FinancialTransactionsStatisticsQuery query)
        {
            return await Mediator.Send(query ?? new FinancialTransactionsStatisticsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateFinancialTransactionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateFinancialTransactionCommand command)
        {
            if (id != command.TransactionId) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFinancialTransactionCommand { TransactionId = id });
            return NoContent();
        }

        [HttpPost("Export")]
        public async Task<ActionResult<ExportFinancialTransactionQueryViewModel>> Export(ExportFinancialTransactionQuery query)
        {
            return await Mediator.Send(query ?? new ExportFinancialTransactionQuery());
        }
    }

}