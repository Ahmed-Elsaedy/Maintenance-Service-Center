using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Application.Employees.Queries;
using ElarabyCA.Application.Employees.Queries.Search;
using ElarabyCA.Domain.Entities;
using System.Linq;

namespace ElarabyCA.Application.Stores.Queries.Search
{
    public class SearchStoreDto : IMapFrom<Store>
    {
        public int StoreId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EmployeeDto Administrator { get; set; }
        public int TotalBalance { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, SearchStoreDto>()
                .ForMember(dest => dest.Administrator, opt => opt.MapFrom(src => src.AdministratorNavigation))
                .ForMember(dest => dest.TotalBalance, opt => opt.MapFrom(src => src.Inventories.Sum(x => x.CurrentBalance)));
        }
    }
}