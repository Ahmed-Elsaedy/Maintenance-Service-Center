using ElarabyCA.Application.ValueGroups.Queres;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    public class ValueGroupsController : ApiController
    {
        [HttpGet("[action]")]
        public async Task<IList<ValueGroupDto>> GetTransactionTypes()
        {
            return await Mediator.Send(new GetValueGroupsByGroupQuery() { Group = "TransactionType" });
        }

        [HttpGet("[action]")]
        public async Task<IList<ValueGroupDto>> GetReferenceTypes()
        {
            return await Mediator.Send(new GetValueGroupsByGroupQuery() { Group = "ReferenceType" });
        }

        [HttpGet("[action]")]
        public async Task<IList<ValueGroupDto>> GetFinancialTransactionTypes()
        {
            return await Mediator.Send(new GetValueGroupsByGroupQuery() { Group = "FinancialTransactionType" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ValueGroupDto>> GetById(int id)
        {
            return await Mediator.Send(new GetValueGroupByIdQuery() { Id = id });
        }
    }
}