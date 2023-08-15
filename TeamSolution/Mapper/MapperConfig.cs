using AutoMapper;
using TeamSolution.Model;
using TeamSolution.Model.Dto;

namespace TeamSolution.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //CreateMap<Source, Destination>();
            CreateMap<Role, NewRoleReqDto>().ReverseMap();
        }
    }
}
