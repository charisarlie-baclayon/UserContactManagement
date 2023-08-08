using ContactInformation.WebAPI.Dtos.User;

namespace ContactInformation.WebAPI.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserLoginDto userLoginDto);
        Task<UserDto> Register(UserRegistrationDto userRegistrationDto);
    }
}
