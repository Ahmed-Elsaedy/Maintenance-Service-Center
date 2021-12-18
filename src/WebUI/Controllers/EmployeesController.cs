using ElarabyCA.Application.Dashboard.Queries;
using ElarabyCA.Application.Employees.Commands;
using ElarabyCA.Application.Employees.Queries.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        [HttpGet("Search")]
        public async Task<ActionResult<SearchEmployeesViewModel>> Search([FromQuery]SearchEmployeesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateEmployeeCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateEmployeeCommand command)
        {
            if (id != command.Id) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            return NoContent();
        }
    }

}
