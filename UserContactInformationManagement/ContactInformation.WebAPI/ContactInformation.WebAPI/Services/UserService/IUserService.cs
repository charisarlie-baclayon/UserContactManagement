using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUserByToken(string token);
        Task<User> GetUser(User userToGet);
        Task<int> CreateUser(User newUser);
        Task<UserDto> UpdateUser(string token, UserRegistrationDto userToUpdate);
        Task<bool> DeleteUser(int userId);
    }
}
