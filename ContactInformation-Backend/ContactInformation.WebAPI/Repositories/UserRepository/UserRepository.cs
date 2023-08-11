using ContactInformation.WebAPI.Context;
using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactInformation.WebAPI.Repositories.UserRepository
{
    /// <summary>
    /// Repository implementation for user-related data operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ContactInformationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// </summary>
        /// <param name="context">The database context for accessing user-related data.</param>
        public UserRepository(ContactInformationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<int> CreateUser(User newUser)
        {
            _context.Users.Add(newUser);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return newUser.Id;
            }
            return 0;
        }

        /// <inheritdoc />
        public async Task<User> UpdateUser(User updateUser)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == updateUser.Id);

            if (user == null)
            {
                return null!;
            }

            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.Username = updateUser.Username;
            user.PasswordHash = updateUser.PasswordHash;
            user.PasswordSalt = updateUser.PasswordSalt;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return user;
            }
            return null!;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId );

            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public async Task<User> GetUser(User userToGet)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(userToGet.Username));
            return user!;
        }

        /// <inheritdoc />
        public async Task<User> GetUserById(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user!;
        }
    }
}
