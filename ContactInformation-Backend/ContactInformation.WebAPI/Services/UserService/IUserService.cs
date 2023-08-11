using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.UserService
{
    /// <summary>
    /// Represents a service that provides operations related to users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets a user using the provided User model.
        /// </summary>
        /// <param name="userToGet">The User model used to retrieve the user.</param>
        /// <returns>Returns a User model with the corresponding Id.</returns>
        Task<User> GetUser(User userToGet);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="newUser">The User object containing information for the new user.</param>
        /// <returns>Returns the Id of the newly created user.</returns>
        Task<int> CreateUser(User newUser);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="userId">The Id of the user to update.</param>
        /// <param name="userToUpdate">The UserRegistrationDto containing updated user information.</param>
        /// <returns>Returns a UserDto with the updated user information.</returns>
        Task<UserDto> UpdateUser(int userId, UserRegistrationDto userToUpdate);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The Id of the user to delete.</param>
        /// <returns>Returns a boolean indicating whether the user was successfully deleted.</returns>
        Task<bool> DeleteUser(int userId);

        /// <summary>
        /// Retrieves the Id of the currently authenticated user.
        /// </summary>
        /// <returns>Returns the Id of the currently authenticated user.</returns>
        Task<int> GetUserId();

        /// <summary>
        /// Retrieves a user by their Id.
        /// </summary>
        /// <param name="userId">The Id of the user to retrieve.</param>
        /// <returns>Returns a UserDto containing the user's information.</returns>
        Task<UserDto> GetUserById(int userId);
    }
}
