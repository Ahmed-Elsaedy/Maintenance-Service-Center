﻿using AutoMapper;
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
    public class TransactionsByStoreIdsQuery : IRequest<TransactionsByStoreIdsQueryViewModel>
    {
        public List<int> StoreIds { get; set; } = new List<int>();
        public int Months { get; set; }
        public int Days { get; set; }
    }

    public class TransactionsByStoreIdsQueryViewModel
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }

    public class TransactionsByStoreIdsQueryHandler : IRequestHandler<TransactionsByStoreIdsQuery, TransactionsByStoreIdsQueryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransactionsByStoreIdsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionsByStoreIdsQueryViewModel> Handle(TransactionsByStoreIdsQuery request, CancellationToken cancellationToken)
        {
            var result = new TransactionsByStoreIdsQueryViewModel();
            result.Data = new List<int>();
            result.Labels = new List<string>();

            var inventoriesIds = _context.Inventory.Where(x => request.StoreIds.Contains(x.StoreId))
                                      .Select(x => x.InventoryId).ToList();

            var groupResult = _context.InventoryTransaction
                    .Where(x => inventoriesIds.Contains(x.InventoryId) && x.Created > DateTime.Now.AddMonths(request.Months).AddDays(request.Days))
                    .GroupBy(f => f.Inventory.Store.Title)
                    .Select(g => new { Store = g.Key, Count = g.Count() });

            //var categoryIds = groupResult.Select(x => x.CategoryId).ToList();
            //var categories = _context.TicketCategory.Where(x => categoryIds.Contains(x.Oid));

            //groupResult.ToList().ForEach(g =>
            //{
            //    var cat = categories.SingleOrDefault(x => x.Oid == g.CategoryId);
            //    result.Labels.Add(cat == null ? "" : cat.Title);
            //    result.Data.Add(g.Count);
            //});

            groupResult.ToList().Select(g =>
            {
                //var cat = categories.SingleOrDefault(x => x.Oid == g.CategoryId);
                return new { Label = g.Store, Data = g.Count };
            }).OrderByDescending(x => x.Data).ToList().ForEach(g => { result.Labels.Add(g.Label); result.Data.Add(g.Data); });


            return await Task.FromResult(result);
        }
    }
}
