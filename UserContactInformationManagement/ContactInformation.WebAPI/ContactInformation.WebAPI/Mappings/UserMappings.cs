using AutoMapper;
using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserRegistrationDto>();
        }
    }
}
