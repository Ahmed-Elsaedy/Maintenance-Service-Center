using AutoMapper;
using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.Employees.Queries.Search
{
    public class EmployeeDto : IMapFrom<Employee>
    {
        public int Oid { get; set; }
        public string DisplayName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDto>();
        }
    }
}
