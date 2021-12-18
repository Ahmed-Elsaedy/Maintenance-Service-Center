using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElarabyCA.Application.Common.Base;
using ElarabyCA.Application.Stores.Commands;
using ElarabyCA.Application.Stores.Queries;
using ElarabyCA.Application.Stores.Queries.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElarabyCA.WebUI.Controllers
{
    [Authorize]
    public class StoresController : ApiController
    {
        [HttpPost("Search")]
        public async Task<ActionResult<SearchStoresQueryViewModel>> Search(SearchStoresQuery query)
        {
            return await Mediator.Send(query ?? new SearchStoresQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SearchStoreDto>> GetById(int id)
        {
            return await Mediator.Send(new GetStoreById() { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateStoreCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateStoreCommand command)
        {
            if (id != command.StoreId) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteStoreCommand { Id = id });
            return NoContent();
        }
    }
}