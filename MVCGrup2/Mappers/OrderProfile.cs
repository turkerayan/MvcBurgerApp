﻿using AutoMapper;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Models;

namespace MVCGrup2.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderViewModel>().ForMember(vm => vm.Id, opt => opt.MapFrom(e => e.Id))
                                             .ForMember(vm => vm.OrderDate, opt => opt.MapFrom(e => e.OrderDate))
                                             .ForMember(vm => vm.OrderStatus, opt => opt.MapFrom(e => e.Id))
                                             .ForMember(vm => vm.Id, opt => opt.MapFrom(e => e.Id))
                                             .ForMember(vm => vm.Id, opt => opt.MapFrom(e => e.Id))
                                             .ForMember(vm => vm.Id, opt => opt.MapFrom(e => e.Id))
                                             .ForMember(vm => vm.Id, opt => opt.MapFrom(e => e.Id))
                                             .ForMember(vm => vm.Id, opt => opt.MapFrom(e => e.Id));
        }

    }
}
