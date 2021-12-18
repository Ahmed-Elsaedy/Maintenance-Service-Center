using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Application.Employees.Queries;
using ElarabyCA.Domain.Entities;
using System.Linq;

namespace ElarabyCA.Application.SpareParts.Queries.Search
{
    public class SearchSparePartDto : IMapFrom<SparePart>
    {
        public int SparePartId { get; set; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public int? Category { get; set; }
        public string Description { get; set; }
        public int TotalBalance { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SparePart, SearchSparePartDto>()
            .ForMember(dest => dest.TotalBalance, opt => opt.MapFrom(src => src.Inventories.Sum(x => x.CurrentBalance)));
        }
    }
}