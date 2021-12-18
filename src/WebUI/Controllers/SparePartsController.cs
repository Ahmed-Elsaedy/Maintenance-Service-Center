using ElarabyCA.Application.SpareParts.Commands;
using ElarabyCA.Application.SpareParts.Queries;
using ElarabyCA.Application.SpareParts.Queries.Search;
using ElarabyCA.Application.Stores.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    [Authorize]
    public class SparePartsController : ApiController
    {
        [HttpPost("Search")]
        public async Task<ActionResult<SearchSparePartsViewModel>> Search(SearchSparePartsQuery query)
        {
            return await Mediator.Send(query ?? new SearchSparePartsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SparePartDto>> GetById(int id)
        {
            return await Mediator.Send(new GetSparePartById() { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSparePartCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateSparePartCommand command)
        {
            if (id != command.SparePartId) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSparePartCommand { Id = id });
            return NoContent();
        }
    }

}
