using AutoMapper;
using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Repositories.UserRepository;

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

        public async Task<UserDto> UpdateUser(string token, UserRegistrationDto userToUpdate)
        {
            var user = await _userRepository.GetUserByToken(token);
            if(user == null)
            {
                return null!;
            }
            var newUserModel = _mapper.Map<User>(userToUpdate);

            var newUserResponse = await _userRepository.UpdateUser(newUserModel);
            if(newUserResponse != null)
            {
                return null!;
            }
            return _mapper.Map<UserDto>(newUserResponse);
        }
    }
}
