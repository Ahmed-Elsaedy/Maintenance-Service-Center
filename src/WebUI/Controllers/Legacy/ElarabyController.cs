using AutoMapper;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using ElarabyCA.Infrastructure.Services;
using ElarabyCore2.MiddleTier.ViewModels;
using ElarabyCore3.Web.Infrastructure;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;

namespace ElarabyCore3.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ElarabyController : ControllerBase
    {
        private const string AuthenticationKey = "ELARABY_AUTH";
        private readonly IApplicationDbContext _Context;
        private readonly IMapper _Mapper;

        public ElarabyController(IApplicationDbContext context, IMapper mapper)
        {
            _Context = context;
            _Mapper = mapper;
        }

        [HttpGet]
        public ActionResult WorkOrders()
        {
            var auth = HttpContext.Session.Get<ElarabyRESTAuthentication>(AuthenticationKey);
            ElarabyRESTService elaraby = new ElarabyRESTService() { Authentication = auth };
            if (auth == null) elaraby.NavigateToHomePage();

            var pageResponse = elaraby.NavigateToWorkOrdersPage();
            HttpContext.Session.Set(AuthenticationKey, elaraby.Authentication);

            var table = pageResponse.GetElementByTagName("table");

            HtmlDocument doc = new HtmlDocument(); doc.LoadHtml(table);
            var wordOrderInputs = doc.DocumentNode.SelectNodes("//input[@id='DeletedItem']");
            if (wordOrderInputs != null)
            {
                var workOrderIds = wordOrderInputs.Select(x => new Guid(x.Attributes["value"]?.Value)).ToList();

                var mapping = _Context.Order.Where(x => x.WorkOrderId.HasValue && workOrderIds.Contains(x.WorkOrderId.Value))
                                        .Select(x => new { x.Oid, x.WorkOrderId }).ToList();
                mapping.ForEach(y =>
                    wordOrderInputs.Single(x => x.Attributes["value"]?.Value == y.WorkOrderId.ToString())
                                    .SetAttributeValue("OID", y.Oid.ToString()));

                table = doc.DocumentNode.OuterHtml;
            }
            return Content(table, "text/html");
        }

        [HttpGet("{id}")]
        public ActionResult GetWorkOrderById(string id)
        {
            var auth = HttpContext.Session.Get<ElarabyRESTAuthentication>(AuthenticationKey);
            ElarabyRESTService elaraby = new ElarabyRESTService() { Authentication = auth };
            if (auth == null) elaraby.NavigateToHomePage();

            var pageResponse = elaraby.NavigateToEditWorkOrderPage(id);
            HttpContext.Session.Set(AuthenticationKey, elaraby.Authentication);

            var result = pageResponse.GetElementByTagName("table");
            return Content(result, "text/html");
        }

        [HttpPost]
        public ActionResult Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                Order data = _Mapper.Map<Order>(model);
                data.DateAssigned = DateTime.Now;
                _Context.Order.Add(data);
                _Context.SaveChangesAsync(new CancellationToken()).Wait();
                return Ok(data.Oid);
            }
            return BadRequest();
        }
    }
}