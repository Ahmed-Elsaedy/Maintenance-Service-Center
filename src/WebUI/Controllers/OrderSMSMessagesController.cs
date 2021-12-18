using ElarabyCA.Application.OrderSMSMessages.Commands;
using ElarabyCA.Application.OrderSMSMessages.Queries.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    public class OrderSMSMessagesController : ApiController
    {
        [HttpPost("Search")]
        public async Task<ActionResult<SearchOrderSMSMessagesQueryViewModel>> Search(SearchOrderSMSMessagesQuery query)
        {
            return await Mediator.Send(query ?? new SearchOrderSMSMessagesQuery());
        }

        [HttpPost("AddOrders")]
        public async Task<ActionResult> AddOrders(AddOrdersForSMSMessageCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateOrderPhonesCommand command)
        {
            if (id != command.OrderId) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(List<int> ids)
        {
            await Mediator.Send(new DeleteOrderSMSMessageCommand { Ids = ids });
            return NoContent();
        }
    }
}
