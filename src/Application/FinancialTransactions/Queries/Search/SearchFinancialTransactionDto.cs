using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;
using System;

namespace ElarabyCA.Application.Transactions.Queries.Search
{
    public class SearchFinancialTransactionDto : IMapFrom<FinancialTransaction>
    {
        public int TransactionId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Title { get; set; }
        public int? Type { get; set; }
        public string TypeValue { get; set; }
        public int? Amount { get; set; }
        public string Remarks { get; set; }
        public DateTime Date { get; set; }

        public DateTime? Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FinancialTransaction, SearchFinancialTransactionDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.EmployeeNavigation.DisplayName))
                .ForMember(dest => dest.TypeValue, opt => opt.MapFrom(src => src.TypeNavigation.Value));
        }
    }
}