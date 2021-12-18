using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElarabyCA.Application.Common.Base;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;


namespace ElarabyCA.Application.Dashboard.Queries
{
    public class LowestSparesByStoreIdsQuery : IRequest<LowestSparesByStoreIdsQueryViewModel>
    {
        public List<int> StoreIds { get; set; }
    }

    public class LowestSparesByStoreIdsQueryViewModel
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }

    public class LowestSparesByStoreIdsQueryHandler : IRequestHandler<LowestSparesByStoreIdsQuery, LowestSparesByStoreIdsQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LowestSparesByStoreIdsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LowestSparesByStoreIdsQueryViewModel> Handle(LowestSparesByStoreIdsQuery request, CancellationToken cancellationToken)
        {
            var result = new LowestSparesByStoreIdsQueryViewModel();
            result.Data = new List<int>();
            result.Labels = new List<string>();

            var invIds = new List<int>();
            IQueryable<Store> query = _context.Store;
            if (request.StoreIds != null && request.StoreIds.Count > 0)
                query = query.Where(x => request.StoreIds.Contains(x.StoreId));

            query.Select(x => x.Inventories.OrderBy(y => y.CurrentBalance).Select(x => x.InventoryId).Take(5)).ToList().ForEach(x => invIds.AddRange(x));

            var inventories = _context.Inventory.Where(x => invIds.Contains(x.InventoryId)).Include("SparePart").OrderByDescending(x => x.CurrentBalance).ToList();
            foreach (var inv in inventories)
            {
                result.Labels.Add(inv.SparePart.Title);
                result.Data.Add(inv.CurrentBalance);
            }

            return await Task.FromResult(result);
        }
    }
}
