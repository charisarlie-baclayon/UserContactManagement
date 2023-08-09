using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUserByToken(string token);
        Task<User> GetUser(User userToGet);
        Task<int> CreateUser(User newUser);
        Task<UserDto> UpdateUser(int userId, UserRegistrationDto userToUpdate);
        Task<bool> DeleteUser(int userId);
        Task<int> GetUserId();
        Task<UserDto> GetUserById(int userId);
    }
}
