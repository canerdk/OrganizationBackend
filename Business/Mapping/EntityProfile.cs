using AutoMapper;
using Entities.Dtos.Auth;
using Entities.Entities;

namespace Business.Mapping
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {

            CreateMap<UserDto, AppUser>().ReverseMap();
            CreateMap<UpdateUserDto, AppUser>().ReverseMap();

        }
    }
}
