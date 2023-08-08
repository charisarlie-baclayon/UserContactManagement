using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<int> CreateUser (User newUser);
        Task<User> GetUser (string token);
        Task<User> UpdateUser (User updateUser);
        Task<bool> DeleteUser (string token);
    }
}
