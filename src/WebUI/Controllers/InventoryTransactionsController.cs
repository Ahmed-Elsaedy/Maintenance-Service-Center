using ElarabyCA.Application.InventoryTransactions.Commands;
using ElarabyCA.Application.Transactions.Queries.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    public class InventoryTransactionsController : ApiController
    {
        [HttpPost("Search")]
        public async Task<ActionResult<SearchITransactionsQueryViewModel>> Search(SearchITransactionsQuery query)
        {
            return await Mediator.Send(query ?? new SearchITransactionsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateInventoryTransactionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateInventoryTransactionCommand command)
        {
            if (id != command.Id) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteInventoryTransactionCommand { Id = id });
            return NoContent();
        }
    }

}