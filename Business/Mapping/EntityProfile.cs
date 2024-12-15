using AutoMapper;
using Entities.Dtos.Auth;
using Entities.Dtos.Contract;
using Entities.Dtos.Event;
using Entities.Dtos.Organization;
using Entities.Dtos.Participant;
using Entities.Dtos.Singer;
using Entities.Dtos.UserContract;
using Entities.Entities;

namespace Business.Mapping
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<UserDto, AppUser>().ReverseMap();
            CreateMap<SingerDto, AppUser>().ReverseMap();
            CreateMap<UpdateUserDto, AppUser>().ReverseMap();
            CreateMap<Contract, ContractDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Organization, OrganizationDto>().ReverseMap();
            CreateMap<Participant, ParticipantDto>().ReverseMap();
            CreateMap<UserContract, UserContractDto>().ReverseMap();
            CreateMap<UserContract, UserContractDetailDto>().ReverseMap();
        }
    }
}
