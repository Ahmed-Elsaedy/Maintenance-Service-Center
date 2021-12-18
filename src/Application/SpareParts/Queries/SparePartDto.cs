using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.SpareParts.Queries
{
    public class SparePartDto : IMapFrom<SparePart>
    {
        public int SparePartId { get; set; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SparePart, SparePartDto>();
        }
    }
}
