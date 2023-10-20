using AutoMapper;
using TeamSolution.Model;
using TeamSolution.ViewModel.Role;
using TeamSolution.ViewModel.Store;

namespace TeamSolution.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //CreateMap<Source, Destination>();
            CreateMap<Role, NewRoleReqDto>().ReverseMap();
            CreateMap<Store, StoreModel>().ReverseMap();
        }
    }
}
