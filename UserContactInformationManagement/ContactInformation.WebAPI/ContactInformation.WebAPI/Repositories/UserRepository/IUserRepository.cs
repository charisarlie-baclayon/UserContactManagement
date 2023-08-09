using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<int> CreateUser (User newUser);
        Task<User> GetUser (User userToGet);
        Task<User> GetUserById(int userId);
        Task<User> UpdateUser (User updateUser);
        Task<bool> DeleteUser (int userId);
    }
}
