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

namespace ElarabyCA.Application.SpareParts.Queries.Search
{
    public class SearchSparePartsQuery : SearchQuery, IRequest<SearchSparePartsViewModel>
    {
    }

    public class SearchSparePartsQueryHandler : IRequestHandler<SearchSparePartsQuery, SearchSparePartsViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchSparePartsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchSparePartsViewModel> Handle(SearchSparePartsQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchSparePartsViewModel();

            IQueryable<SparePart> query = _context.SparePart;

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var keys = request.Filter.ToLower().Split(' ').Select(x => x.Trim());
                foreach (var k in keys)
                {
                    query = query.Where(x =>
                            x.Title.ToLower().Contains(k) ||
                            //x.SparePartId.ToString().Contains(k) ||
                            x.Description.ToLower().Contains(k) ||
                            x.Barcode.ToLower().Contains(k));
                }
            }


            result.TotalRecords = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Sort))
                query = query.OrderBy(request.Sort);

            if (request.PageIndex.HasValue && request.PageLength.HasValue)
            {
                query = query.Skip(request.PageIndex.Value * request.PageLength.Value)
                            .Take(request.PageLength.Value);
            }

            result.Data = await query
                        .ProjectTo<SearchSparePartDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

            return result;
        }
    }
}
