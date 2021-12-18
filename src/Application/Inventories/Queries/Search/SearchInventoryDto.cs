using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Application.Employees.Queries;
using ElarabyCA.Domain.Entities;

namespace ElarabyCA.Application.Inventories.Queries.Search
{
    public class SearchInventoryDto : IMapFrom<Inventory>
    {
        public int InventoryId { get; set; }

        public int StoreId { get; set; }

        public string StoreTitle { get; set; }

        public string SparePartBarcode { get; set; }
        public int SpareItemId { get; set; }
        public string SparePartTitle { get; set; }

        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Inventory, SearchInventoryDto>()
                .ForMember(dest => dest.StoreTitle, opt => opt.MapFrom(src => src.Store.Title))
                .ForMember(dest => dest.SparePartTitle, opt => opt.MapFrom(src => src.SparePart.Title))
                .ForMember(dest => dest.SpareItemId, opt => opt.MapFrom(src => src.SparePartId))
                .ForMember(dest => dest.SparePartBarcode, opt => opt.MapFrom(src => src.SparePart.Barcode));
        }
    }
}