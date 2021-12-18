using ElarabyCA.Application.Dashboard.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    public class DashboardController : ApiController
    {
        [HttpGet("OrdersByCategory")]
        public async Task<ActionResult<OrdersByCategoryQueryViewModel>> OrdersByCategory([FromQuery]OrdersByCategoryQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("OrdersRateOverTime")]
        public async Task<ActionResult<OrdersRateOverTimeViewModel>> OrdersRateOverTime([FromQuery]OrdersRateOverTimeQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("LowestSparesByStoreIdsQuery")]
        public async Task<ActionResult<LowestSparesByStoreIdsQueryViewModel>> LowestSparesByStoreIds([FromQuery]LowestSparesByStoreIdsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("TransactionsRateOverTime")]
        public async Task<ActionResult<TransactionsRateOverTimeViewModel>> TransactionsRateOverTime([FromQuery]TransactionsRateOverTimeQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("OrderSMSRateOverTime")]
        public async Task<ActionResult<OrderSMSRateOverTimeViewModel>> OrderSMSRateOverTime([FromQuery]OrderSMSRateOverTimeQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("AvailableSmsBalance")]
        public async Task<ActionResult<AvailableSmsBalanceViewModel>> AvailableSmsBalance([FromQuery]AvailableSmsBalanceQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("TicketsByEmployees")]
        public async Task<ActionResult<TicketsByEmployeesQueryViewModel>> TicketsByEmployees([FromQuery]TicketsByEmployeesQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
