using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.SMSMessages.Queries.Search
{
    public class SearchSMSMessageDto : IMapFrom<SMSMessage>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int OrdersCount { get; set; }
        public DateTime? Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SMSMessage, SearchSMSMessageDto>()
                .ForMember(dest => dest.OrdersCount, opt => opt.MapFrom(src => src.OrderSMSMessages.Count));
        }
    }
}
