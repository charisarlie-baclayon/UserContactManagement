using ContactInformation.WebAPI.Context;
using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ContactInformation.WebAPI.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ContactInformationDbContext _context;

        public UserRepository(ContactInformationDbContext context)
        {
            _context = context;
        }

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

        public async Task<User> GetUser(User userToGet)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(userToGet.Username));
            return user!;
        }
        public async Task<User> GetUserById(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user!;
        }
    }
}
