using AutoMapper;
using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Repositories.UserRepository;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ContactInformation.WebAPI.Services.UserService
{
    /// <summary>
    /// Provides method implementation from IUserService.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the UserService class.
        /// </summary>
        /// <param name="mapper">An instance of AutoMapper.</param>
        /// <param name="userRepository">An instance of the user repository.</param>
        /// <param name="httpContextAccessor">An instance of IHttpContextAccessor for accessing the HttpContext.</param>
        public UserService(IMapper mapper, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public async Task<int> CreateUser(User newUser)
        {
            var user = await _userRepository.GetUser(newUser);
            if(user != null)
            {
                return 0;
            }
            return await _userRepository.CreateUser(newUser);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteUser(int userId)
        {
            var res = await _userRepository.DeleteUser(userId);
            if (!res)
            {
                throw new UserDeletionFailedException("Address deletion failed.");
            }
            return res;
        }

        /// <inheritdoc/>
        public async Task<UserDto> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new UserNotFoundException($"User with ID {userId} not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc/>
        public async Task<User> GetUser(User userToGet)
        {
            var user = await _userRepository.GetUser(userToGet);
            return user;
        }

        /// <inheritdoc/>
        public async Task<UserDto> UpdateUser(int userId, UserRegistrationDto userToUpdate)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new UserNotFoundException($"User with ID {userId} not found.");
            }

            var userModel = _mapper.Map<User>(userToUpdate);

            CreatePasswordHash(userToUpdate.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userModel.Id = userId;
            userModel.FirstName = userToUpdate.FirstName;
            userModel.LastName = userToUpdate.LastName;
            userModel.Username = userToUpdate.Username;
            userModel.PasswordHash = passwordHash;
            userModel.PasswordSalt = passwordSalt;

            var newUserResponse = await _userRepository.UpdateUser(userModel);
            if (newUserResponse == null)
            {
                throw new UserUpdateFailedException("User update failed.");
            }

            return _mapper.Map<UserDto>(newUserResponse);
        }

        /// <inheritdoc/>
        public async Task<int> GetUserId()
        {
            var result =  _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
            if(result == null)
            {
                return 0;
            }
            var userClaims = result.Claims;
            var user = new User
            {
                Username = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value,
                Id = Convert.ToInt32(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value)
            };
            return user.Id;
        }

        /// <summary>
        /// Creates a password hash and salt using HMACSHA512 algorithm.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <param name="passwordHash">The resulting password hash.</param>
        /// <param name="passwordSalt">The resulting password salt.</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
