using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;
using System;

namespace ElarabyCA.Application.Transactions.Queries.Search
{
    public class SearchITransactionDto : IMapFrom<InventoryTransaction>
    {
        public int TransactionId { get; set; }
        public int InventoryId { get; set; }

        public string StoreTitle { get; set; }
        public string StoreAdministrator { get; set; }
        public string SparePartTitle { get; set; }
        public string SparePartBarcode { get; set; }

        public int? Type { get; set; }
        public string TypeValue { get; set; }

        public int? Amount { get; set; }
        public string Description { get; set; }

        public DateTime? Created { get; set; }

        public int? ReferenceType { get; set; }
        public string ReferenceTypeValue { get; set; }
        public string ReferenceId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InventoryTransaction, SearchITransactionDto>()
                .ForMember(dest => dest.StoreTitle, opt => opt.MapFrom(src => src.Inventory.Store.Title))
                .ForMember(dest => dest.StoreAdministrator, opt => opt.MapFrom(src => src.Inventory.Store.AdministratorNavigation.DisplayName))
                .ForMember(dest => dest.SparePartTitle, opt => opt.MapFrom(src => src.Inventory.SparePart.Title))
                .ForMember(dest => dest.SparePartBarcode, opt => opt.MapFrom(src => src.Inventory.SparePart.Barcode))
                .ForMember(dest => dest.TypeValue, opt => opt.MapFrom(src => src.TypeNavigation.Value));
        }
    }
}