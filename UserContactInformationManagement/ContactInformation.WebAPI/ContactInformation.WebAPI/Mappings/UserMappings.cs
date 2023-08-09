using AutoMapper;
using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Mappings
{
    /// <summary>
    /// Provides mapping profiles for User related entities.
    /// </summary>
    public class UserMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the UserMappings class.
        /// Defines mapping configurations between User-related DTOs and entities.
        /// </summary>
        public UserMappings()
        {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserRegistrationDto>();
            CreateMap<User, UserLoginDto>();
        }
    }
}
