using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.SMSMessages.Queries.Search
{
    public class SMSMessageDto : IMapFrom<SMSMessage>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SMSMessage, SMSMessageDto>();
        }
    }
}
