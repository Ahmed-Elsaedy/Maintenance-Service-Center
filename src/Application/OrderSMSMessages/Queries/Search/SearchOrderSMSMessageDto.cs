using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Application.Employees.Queries;
using ElarabyCA.Domain.Entities;
using System;

namespace ElarabyCA.Application.OrderSMSMessages.Queries.Search
{
    public class SearchOrderSMSMessageDto : IMapFrom<OrderSMSMessage>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Customer { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public int SMSMessageId { get; set; }
        public DateTime? LastSent { get; set; }
        public int SendCount { get; set; }
        public string Report { get; set; }
        public string Phone { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderSMSMessage, SearchOrderSMSMessageDto>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Order.Customer))
                .ForMember(dest => dest.PrimaryPhone, opt => opt.MapFrom(src => src.Order.PrimaryPhone))
                .ForMember(dest => dest.SecondaryPhone, opt => opt.MapFrom(src => src.Order.SecondaryPhone));
        }
    }
}