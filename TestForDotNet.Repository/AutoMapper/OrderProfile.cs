using AutoMapper;
using TestForDotNet.Core.DTOs;
using TestForDotNet.Repository.Models;

namespace TestForDotNet.Repository.AutoMapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().PreserveReferences();
            CreateMap<OrderDTO, Order>()
                .ForMember(x => x.Windows, opt => opt.MapFrom(src => new List<WindowDTO>()));

            CreateMap<Window, WindowDTO>();
            CreateMap<WindowDTO, Window>()
                .ForMember(dest => dest.SubElements, opt => opt.MapFrom(opt => new List<SubElementDTO>()))
                .ForMember(dest => dest.Order, opt => opt.Ignore());

            CreateMap<SubElement, SubElementDTO>().PreserveReferences();
            CreateMap<SubElementDTO, SubElement>()
                .ForMember(x => x.Window, opt => opt.Ignore());
        }
    }
}