using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCore2.DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueryDesignerCore;

namespace ElarabyCore3.Web.Infrastructure
{
    interface IGenericController<T> where T : IDbModel
    {
        Task<ActionResult<IEnumerable<T>>> GetAll();
        Task<ActionResult<T>> GetById(int id);
        Task<IActionResult> Put(int id, T model);
        Task<ActionResult<T>> Post(T model);
        Task<ActionResult<T>> Delete(int id);
    }

    public abstract class GenericControllerBase<TModel, TEditViewModel> : ControllerBase
            where TModel : class, IDbModel
            where TEditViewModel : class
    {
        protected readonly IApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public GenericControllerBase(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Query(FilterContainer filter)
        {
            IQueryable data = _context.Set<TModel>();
            data = data.Cast<TModel>().Request(new FilterContainer()
            {
                Where = new TreeFilter()
                {
                    Field = "Gcrecord",
                    FilterType = WhereFilterType.IsNull
                }
            });

            if (filter != null)
            {
                data = ((IQueryable<TModel>)data).Request(filter);

                if (!string.IsNullOrEmpty(filter.Includes))
                    filter.Includes.Split(',').ToList().ForEach(x =>
                    data = ((IQueryable<TModel>)data).Include(x));


                if (!string.IsNullOrEmpty(filter.Select))
                    data = data.Select($"new({filter.Select})");
            }

            return new JsonResult(data.ToDynamicList());
        }

        [HttpPost]
        public virtual ActionResult Create(TEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                TModel data = _mapper.Map<TModel>(model);
                _context.Set<TModel>().Add(data);
                _context.SaveChangesAsync(new CancellationToken()).Wait();

                return Ok(data.Oid);
            }
            return BadRequest();
        }

        [HttpPut]
        public virtual IActionResult Update(int id, TEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                TModel data = _mapper.Map<TModel>(model);
                if (data.Oid == id)
                {
                    var target = _context.Set<TModel>().Find(id);
                    if (target != null)
                    {
                        _context.Entry(target).CurrentValues.SetValues(data);
                        _context.Entry(target).State = EntityState.Modified;
                        _context.SaveChangesAsync(new CancellationToken()).Wait();

                        return Ok();
                    }
                    else
                        return NotFound();
                }
            }
            return BadRequest();
        }

        protected virtual IQueryable OnConfigureQueryableData(IQueryable data)
        {
            return data;
        }
    }
}