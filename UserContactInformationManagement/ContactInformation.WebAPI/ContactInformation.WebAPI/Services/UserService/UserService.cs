using AutoMapper;
using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Repositories.UserRepository;
using System.Security.Cryptography;

namespace ContactInformation.WebAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<int> CreateUser(User newUser)
        {
            var user = await _userRepository.GetUser(newUser);
            if(user != null)
            {
                return 0;
            }
            return await _userRepository.CreateUser(newUser);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var res = await _userRepository.DeleteUser(userId);
            return res;
        }

        public async Task<UserDto> GetUserByToken(string token)
        {
            var user = await _userRepository.GetUserByToken(token);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<User> GetUser(User userToGet)
        {
            var user = await _userRepository.GetUser(userToGet);
            return user;
        }

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
