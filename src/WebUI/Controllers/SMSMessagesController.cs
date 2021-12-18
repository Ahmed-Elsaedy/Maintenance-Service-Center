using ElarabyCA.Application.OrderSMSMessages.Queries.Search;
using ElarabyCA.Application.SMSMessages.Commands;
using ElarabyCA.Application.SMSMessages.Queries;
using ElarabyCA.Application.SMSMessages.Queries.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    public class SMSMessagesController : ApiController
    {
        [HttpPost("Search")]
        public async Task<ActionResult<SearchSMSMessagesViewModel>> Search(SearchSMSMessagesQuery query)
        {
            return await Mediator.Send(query ?? new SearchSMSMessagesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SMSMessageDto>> GetById(int id)
        {
            return await Mediator.Send(new GetSMSMessageByIdQuery() { Id = id });
        }

        [HttpPost("Send")]
        public async Task<ActionResult<SearchOrderSMSMessageDto>> Send(SendSMSMessageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSMSMessageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateSMSMessageCommand command)
        {
            if (id != command.Id) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSMSMessageCommand { Id = id });
            return NoContent();
        }
    }
}