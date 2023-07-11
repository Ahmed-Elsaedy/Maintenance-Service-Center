using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using ElarabyCore2.DataLayer.Interfaces;
using ElarabyCore2.MiddleTier.Annotations;
using ElarabyCore2.MiddleTier.DataTable;
using ElarabyCore2.MiddleTier.ViewModels;
using ElarabyCore2.WebApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QueryDesignerCore;

namespace ElarabyCore2.WebApp.Controllers
{
    //[Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataTableController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DataTableController(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Orders(DataTableRequest request)
        {
            var result = PageData<Order, IndexOrderModel>(request);
            return result;
        }

        [HttpPost]
        public IActionResult Tickets(DataTableRequest request)
        {
            var result = PageData<OrderTicket, IndexOrderTicketModel>(request);
            return result;
        }

        [HttpPost]
        public IActionResult Employees(DataTableRequest request)
        {
            var result = PageData<Employee, EmployeeModel>(request);
            return result;
        }

        [HttpPost]
        public IActionResult Categories(DataTableRequest request)
        {
            var result = PageData<TicketCategory, TicketCategoryModel>(request);
            return result;
        }

        private IActionResult PageData<TModel, TIndexViewModel>(DataTableRequest request)
           where TModel : class, IDbModel
           where TIndexViewModel : class
        {
            if(request == null)
            {
                var res = new DataTableResponse();
                res.Data = null;
                res.Draw = 0;
                res.RecordsTotal = 0;
                res.RecordsFiltered = 0;

                return new JsonResult(res);
            }

            IQueryable data = _context.Set<TModel>().AsNoTracking();

            // Filteration
            data = this.OnConfigureFilters<TModel>(data, request);



            // Hide Deleted Rows
            var filter = new FilterContainer()
            {
                Where = new TreeFilter()
                {
                    Field = "Gcrecord",
                    FilterType = WhereFilterType.IsNull
                }
            };
            data = data.Cast<TModel>().Request(filter);

            IQueryable _dataBeforeFiltering = data;

            // Sorting
            data = ApplySorting(data, request);


            // Pagination
            var temp = data.Select("Oid");
            var count = temp.Count();
            temp = temp.Skip(request.Start).Take(request.Length);
            var ids = temp.Cast<int>().ToList();

            data = _context.Set<TModel>().AsNoTracking().Where(x => ids.Contains(x.Oid));
            data = OnConfigureQueryableData<TModel>(data);
            data = ApplySorting(data, request);

            var result = _mapper.Map<IEnumerable<TIndexViewModel>>(data.AsEnumerable());

            var response = new DataTableResponse();
            response.Data = result;
            response.Draw = request.Draw;
            response.RecordsTotal = count;
            response.RecordsFiltered = count;

            response = this.OnConfigureDataTableResponse<TModel>(response, _dataBeforeFiltering);

            return new JsonResult(response);
        }

        private IQueryable OnConfigureFilters<TModel>(IQueryable data, DataTableRequest request) where TModel : class, IDbModel
        {
            if (request == null)
                return data;
            switch (typeof(TModel).Name)
            {
                case nameof(Order):
                    var result = data.Cast<Order>();
                    if (request.AdditionalParameters != null)
                    {
                        if (request.AdditionalParameters.ContainsKey("ticket"))
                        {
                            var tickets = JsonConvert.DeserializeObject<List<string>>(
                                request.AdditionalParameters["ticket"].ToString()).Select(x => int.Parse(x));

                            var orders = _context.OrderTicket.Where(x => x.IsActive == true && x.Category.HasValue && tickets.Contains(x.Category.Value)).Select(x => x.Order);

                            result = _context.Order.Where(x => orders.Contains(x.Oid));


                            //var filter = new FilterContainer()
                            //{
                            //    Where = new TreeFilter()
                            //    {
                            //        OperatorType = TreeFilterType.And,
                            //        Operands = new List<TreeFilter>()
                            //      {
                            //          new TreeFilter()
                            //          {
                            //               Field = "ActiveTicket",
                            //               FilterType = WhereFilterType.IsNotNull
                            //          },
                            //          new TreeFilter()
                            //          {
                            //               Field = "ActiveTicketNavigation.Category",
                            //               FilterType = WhereFilterType.IsNotNull
                            //          },
                            //          new TreeFilter()
                            //          {
                            //                OperatorType = TreeFilterType.Or,
                            //                Operands = tickets.Select(x => new TreeFilter()
                            //                {
                            //                    Field = "ActiveTicketNavigation.CategoryNavigation.Oid",
                            //                    FilterType = WhereFilterType.Equal,
                            //                    Value = x
                            //                }).ToList()
                            //          }
                            //    }
                            //    }
                            //};
                            //result = result.Request(filter);
                        }

                        if (request.AdditionalParameters.ContainsKey("oid"))
                        {
                            if (int.TryParse(request.AdditionalParameters["oid"].ToString(), out int oid))
                            {
                                result = result.Where(x => x.Oid == oid);
                            }
                        }

                        if (request.AdditionalParameters.ContainsKey("customer"))
                            result = result.Where(x => !string.IsNullOrEmpty(x.Customer) &&
                            x.Customer.Contains(request.AdditionalParameters["customer"].ToString()));

                        if (request.AdditionalParameters.ContainsKey("phones"))
                            result = result.Where(x => (
                            !string.IsNullOrEmpty(x.PrimaryPhone) &&
                            x.PrimaryPhone.Contains(request.AdditionalParameters["phones"].ToString()))
                            || (!string.IsNullOrEmpty(x.SecondaryPhone) &&
                            x.SecondaryPhone.Contains(request.AdditionalParameters["phones"].ToString())));

                        if (request.AdditionalParameters.ContainsKey("region"))
                        {
                            var regions = JsonConvert.DeserializeObject<List<string>>(
                                request.AdditionalParameters["region"].ToString());

                            result = result.Where(x => !string.IsNullOrEmpty(x.Region) && regions.Contains(x.Region));
                        }

                        if (request.AdditionalParameters.ContainsKey("note"))
                        {
                            var notes = JsonConvert.DeserializeObject<List<string>>(
                                request.AdditionalParameters["note"].ToString());

                            result = result.Where(x => !string.IsNullOrEmpty(x.Street) && notes.Contains(x.Street));
                        }


                    }
                    if (request.Search != null && !string.IsNullOrEmpty(request.Search.Value))
                    {
                        var splits = request.Search.Value.Split(" ");
                        result = result.Where(x => x.ActiveTicket.HasValue)
                            .Include(x => x.ActiveTicketNavigation)
                            .Where(x => x.ActiveTicketNavigation.Report.Contains(request.Search.Value));
                    }

                    return result;
                default:
                    return data;
            }
        }
        private IQueryable ApplySorting(IQueryable data, DataTableRequest request)
        {
            for (int i = 0; i < request.Order.Count; i++)
            {
                var current = request.Order[i];
                var ordering = request.Columns[current.Column].Name
                    + (current.Dir == "asc" ? "" : " desc");
                if (i == 0)
                    data = data.OrderBy(ordering);
                else
                    data = ((IOrderedQueryable)data).ThenBy(ordering);
            }
            return data;
        }

        private IQueryable OnConfigureQueryableData<TModel>(IQueryable data)
            where TModel : class, IDbModel
        {
            switch (typeof(TModel).Name)
            {
                case nameof(Order):
                    return data.Cast<Order>()
                       .Include(x => x.ActiveTicketNavigation)
                       .Include(x => x.ActiveTicketNavigation.CategoryNavigation)
                       .Include(x => x.ActiveTicketNavigation.EmployeeNavigation);

                case nameof(OrderTicket):
                    return data.Cast<OrderTicket>()
                        .Include(x => x.EmployeeNavigation)
                        .Include(x => x.CategoryNavigation);

                case nameof(Employee):
                case nameof(TicketCategory):
                default:
                    return data;
            }
        }

        private DataTableResponse OnConfigureDataTableResponse<TModel>(
            DataTableResponse response, IQueryable dataBeforeFiltering)
        {
            switch (typeof(TModel).Name)
            {
                case nameof(Order):
                    IQueryable<Order> orders = dataBeforeFiltering.Cast<Order>();
                    var snapshots = orders.Select(x => new { x.Region, x.Street });

                    var regions = snapshots.Select(x => x.Region).Distinct();
                    var notes = snapshots.Select(x => x.Street).Distinct();

                    response.AdditionalParameters = new Dictionary<string, Object>();
                    response.AdditionalParameters.Add("regions", regions);
                    response.AdditionalParameters.Add("notes", notes);
                    break;
            }
            return response;
        }

        private IActionResult FieldsData<TIndexViewModel>() where TIndexViewModel : class
        {
            Type indexModelType = typeof(TIndexViewModel);
            var properties = indexModelType.GetProperties();

            Dictionary<string, int> indexes = new Dictionary<string, int>();
            for (int i = 0; i < properties.Length; i++)
                indexes.Add(properties[i].Name, i);

            var fields = properties.Select(x => new
            {
                Metadata = x.GetCustomAttribute<IndexViewField>(),
                Property = x
            }).ToList();

            var columns = fields.Select(x => new
            {
                Data = x.Property.Name.ToCamelCase(),
                x.Property.Name,
                Title = x.Metadata?.Title ?? x.Property.Name,
                Sortable = x.Metadata?.Sortable ?? false
            });

            var order = fields.Where(x => x.Metadata != null && x.Metadata.SortOrder > 0)
                              .OrderBy(x => x.Metadata.SortOrder)
                              .Select(x => new object[] {
                                  indexes[x.Property.Name],
                                  x.Metadata.SortDirection == FieldSortDirection.Ascending ? "asc": "desc"
                              });
            var jsonResult = new JsonResult(new { columns, order });

            return jsonResult;
        }
    }
}