using ElarabyCA.Application.Inventories.Commands;
using ElarabyCA.Application.Inventories.Queries.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    [Authorize]
    public class InventoriesController : ApiController
    {
        [HttpPost("Search")]
        public async Task<ActionResult<SearchInventoriesQueryViewModel>> Search(SearchInventoriesQuery query)
        {
            return await Mediator.Send(query ?? new SearchInventoriesQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateInventoryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateInventoryCommand command)
        {
            if (id != command.Id) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteInventoryCommand { Id = id });
            return NoContent();
        }
    }

}
