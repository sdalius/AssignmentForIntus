using AutoMapper;
using TestForDotNet.Core.DTOs;
using TestForDotNet.ViewModel;

namespace TestForDotNet.MapperProfile
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile() 
        {
            CreateMap<OrderDTO, OrderViewModel>().PreserveReferences();
            CreateMap<OrderViewModel, OrderDTO>()
                .ForMember(x => x.Windows, opt => opt.MapFrom(src => new List<WindowViewModel>()));

            CreateMap<WindowDTO, WindowViewModel>().PreserveReferences();
            CreateMap<WindowViewModel, WindowDTO>()
                .ForMember(x => x.Order, opt => opt.MapFrom(src => new OrderDTO()))
                .ForMember(x => x.SubElements, opt => opt.MapFrom(src => new List<SubElementDTO>()));
        }
    }
}