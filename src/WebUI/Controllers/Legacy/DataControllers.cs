using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using ElarabyCore2.DataLayer.Interfaces;
using ElarabyCore2.MiddleTier.ViewModels;
using ElarabyCore2.WebApp.Infrastructure;
using ElarabyCore3.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QueryDesignerCore;

namespace ElarabyCore2.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : GenericControllerBase<Order, CreateOrderViewModel>
    {
        public OrderController(IApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

        [HttpPut]
        public ActionResult Patch(PatchOrderEdit patchData)
        {
            if (patchData.OrderFields.Count > 0)
                foreach (int oid in patchData.Orders)
                {
                    Order order = new Order();
                    order.Oid = oid;
                    var entry = _context.Set<Order>().Attach(order);
                    foreach (string field in patchData.OrderFields)
                    {
                        string propName = field.ToPascalCase();
                        typeof(Order).GetProperty(propName).SetValue(order,
                            typeof(Order).GetProperty(propName).GetValue(patchData.TemplateOrder));
                        entry.Property(propName).IsModified = true;
                    }
                }
            if (patchData.TicketFields.Count > 0)
                foreach (int oid in patchData.Tickets)
                {
                    OrderTicket ticket = new OrderTicket();
                    ticket.Oid = oid;
                    var entry = _context.Set<OrderTicket>().Attach(ticket);
                    foreach (string field in patchData.TicketFields)
                    {
                        string propName = field.ToPascalCase();
                        typeof(OrderTicket).GetProperty(propName).SetValue(ticket,
                            typeof(OrderTicket).GetProperty(propName).GetValue(patchData.TemplateTicket));
                        entry.Property(propName).IsModified = true;
                    }
                }
            _context.SaveChangesAsync(new CancellationToken()).Wait();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _context.Set<Order>().Find(id);
            obj.Gcrecord = new Random().Next(10000);
            _context.SaveChangesAsync(new CancellationToken()).Wait();
            return Ok();
        }

    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderTicketController : GenericControllerBase<OrderTicket, OrderTicket>
    {
        public OrderTicketController(IApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

        public override ActionResult Create(OrderTicket model)
        {


            var result = base.Create(model);
            if (result is OkObjectResult)
            {
                var oid = (int)((OkObjectResult)result).Value;
                var order = _context.Set<Order>().Find(model.Order);
                var active = _context.Set<OrderTicket>().Find(oid);
                var tickets = _context.Set<OrderTicket>().Where(x => x.Order == order.Oid);
                foreach (OrderTicket ticket in tickets)
                    ticket.IsActive = ticket.Oid == active.Oid;
                order.ActiveTicket = active.Oid;
                _context.SaveChangesAsync(new CancellationToken()).Wait();
            }
            return result;
        }

        public override IActionResult Update(int id, OrderTicket model)
        {
            return base.Update(id, model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _context.Set<OrderTicket>().Find(id);
            obj.Gcrecord = new Random().Next(10000);
            _context.SaveChangesAsync(new CancellationToken()).Wait();
            return Ok();
        }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : GenericControllerBase<Employee, Employee>
    {
        public EmployeeController(IApplicationDbContext context, IMapper mapper) : base(context, mapper) { }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _context.Set<Employee>().Find(id);
            obj.Gcrecord = new Random().Next(10000);
            _context.SaveChangesAsync(new CancellationToken()).Wait();
            return Ok();
        }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : GenericControllerBase<TicketCategory, TicketCategory>
    {
        public CategoryController(IApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _context.Set<TicketCategory>().Find(id);
            obj.Gcrecord = new Random().Next(10000);
            _context.SaveChangesAsync(new CancellationToken()).Wait();
            return Ok();
        }
    }

    public class PatchOrderEdit
    {
        public List<int> Orders { get; set; }
        public List<int> Tickets { get; set; }
        public List<string> OrderFields { get; set; }
        public List<string> TicketFields { get; set; }

        public Order TemplateOrder { get; set; }
        public OrderTicket TemplateTicket { get; set; }

        public PatchOrderEdit()
        {
            Orders = new List<int>();
            Tickets = new List<int>();
            OrderFields = new List<string>();
            TicketFields = new List<string>();
        }
    }
}
