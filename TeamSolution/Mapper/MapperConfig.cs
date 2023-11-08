using AutoMapper;
using TeamSolution.Model;
using TeamSolution.ViewModel.Order;
using TeamSolution.ViewModel.Role;
using TeamSolution.ViewModel.Store;
using TeamSolution.ViewModel.StoreService;

namespace TeamSolution.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //CreateMap<Source, Destination>();
            CreateMap<Role, NewRoleReqDto>().ReverseMap();
            CreateMap<Store, StoreModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<StoreService, GetStoreServiceReqDto>().ReverseMap();
        }
    }
}
