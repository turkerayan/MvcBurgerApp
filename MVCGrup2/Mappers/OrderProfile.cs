using AutoMapper;
using MVCGrup2.Areas.Customer.Models;
using MVCGrup2.Entities.Concrete;

namespace MVCGrup2.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderViewModel>().ForMember(vm => vm.Id, opt => opt.MapFrom(e => e.Id))
                                             .ForMember(vm => vm.OrderDate, opt => opt.MapFrom(e => e.OrderDate))
                                             .ForMember(vm => vm.OrderStatus, opt => opt.MapFrom(e => e.OrderStatus))
                                             .ForMember(vm => vm.OrderCount, opt => opt.MapFrom(e => e.OrderCount))
                                             .ForMember(vm => vm.Total, opt => opt.MapFrom(e => e.Total))
                                             .ForMember(vm => vm.ExtraMatsViewModel, opt => opt.MapFrom(e => e.ExtraMats))
                                             .ForMember(vm => vm.MenusViewModel, opt => opt.MapFrom(e => e.Menus))
                                             .ReverseMap();
                                             
        }

    }
}
