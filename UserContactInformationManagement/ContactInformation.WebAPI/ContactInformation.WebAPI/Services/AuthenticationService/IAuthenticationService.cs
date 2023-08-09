using ContactInformation.WebAPI.Dtos.User;

namespace ContactInformation.WebAPI.Services.AuthenticationService
{
    /// <summary>
    /// Service interface for user authentication operations.
    /// </summary>
    public interface IAuthenticationService
    {
        // <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="userLoginDto">User login information.</param>
        /// <returns>JWT token.</returns>
        Task<string> Login(UserLoginDto userLoginDto);

        /// <summary>
        /// Registers a new user and returns the registered user's data.
        /// </summary>
        /// <param name="userRegistrationDto">User registration information.</param>
        /// <returns>Registered user data.</returns>
        Task<UserDto> Register(UserRegistrationDto userRegistrationDto);
    }
}
