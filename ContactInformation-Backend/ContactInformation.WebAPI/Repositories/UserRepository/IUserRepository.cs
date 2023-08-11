using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories.UserRepository
{
    /// <summary>
    /// Repository interface for user-related data operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="newUser">The user entity to create.</param>
        /// <returns>The ID of the created user.</returns>
        Task<int> CreateUser (User newUser);

        /// <summary>
        /// Retrieves a user by matching its properties.
        /// </summary>
        /// <param name="userToGet">The user entity with properties to match.</param>
        /// <returns>The retrieved user if found; otherwise, null.</returns>
        Task<User> GetUser (User userToGet);

        /// <summary>
        /// Retrieves a user by its ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The retrieved user if found; otherwise, null.</returns>
        Task<User> GetUserById(int userId);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="updateUser">The user entity with updated properties.</param>
        /// <returns>The updated user entity.</returns>
        Task<User> UpdateUser (User updateUser);

        /// <summary>
        /// Deletes a user by its ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>True if the user was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteUser (int userId);
    }
}
