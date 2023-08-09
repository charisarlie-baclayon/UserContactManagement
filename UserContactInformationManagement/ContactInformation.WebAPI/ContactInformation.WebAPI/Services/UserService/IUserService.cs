using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.UserService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userToGet"></param>
        /// <returns></returns>
        Task<User> GetUser(User userToGet);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        Task<int> CreateUser(User newUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userToUpdate"></param>
        /// <returns></returns>
        Task<UserDto> UpdateUser(int userId, UserRegistrationDto userToUpdate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> GetUserId();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserDto> GetUserById(int userId);
    }
}
