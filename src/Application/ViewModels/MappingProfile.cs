using AutoMapper;
using ElarabyCA.Domain.Entities;
using ElarabyCore2.MiddleTier.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, IndexOrderModel>()
                .ForMember(dest => dest.TicketReport, opt => opt.MapFrom(src => src.ActiveTicketNavigation.Report))
                .ForMember(dest => dest.TicketCategory, opt => opt.MapFrom(src => src.ActiveTicketNavigation.CategoryNavigation.Title))
                .ForMember(dest => dest.TicketEmployee, opt => opt.MapFrom(src => src.ActiveTicketNavigation.EmployeeNavigation.DisplayName));

            CreateMap<CreateOrderViewModel, Order>();

            CreateMap<Employee, EmployeeModel>();
            CreateMap<TicketCategory, TicketCategoryModel>();
            CreateMap<OrderTicket, IndexOrderTicketModel>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.EmployeeNavigation.FullName))
                .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.CategoryNavigation.Title));
        }
    }
}
