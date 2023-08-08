using AutoMapper;
using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Services.UserService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ContactInformation.WebAPI.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _mapper = mapper;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<UserDto> Register(UserRegistrationDto userRegistrationDto)
        {
            var userModel = _mapper.Map<User>(userRegistrationDto);
            var userCheck = await _userService.GetUser(userModel);

            if (userCheck != null)
            {
                throw new UserAlreadyExistsException("User already exists.");
            }

            var newUserModel = _mapper.Map<User>(userRegistrationDto);

            CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            newUserModel.FirstName = userRegistrationDto.FirstName;
            newUserModel.LastName = userRegistrationDto.LastName;
            newUserModel.Username = userRegistrationDto.Username;
            newUserModel.PasswordHash = passwordHash;
            newUserModel.PasswordSalt = passwordSalt;

            var newUserId = await _userService.CreateUser(newUserModel);

            if (newUserId == 0)
            {
                throw new Exception("Error creating user.");
            }

            newUserModel.Id = newUserId;
            return _mapper.Map<UserDto>(newUserModel);
        }

        public async Task<string> Login(UserLoginDto userLoginDto)
        {
            var userModel = _mapper.Map<User>(userLoginDto);
            var user = await _userService.GetUser(userModel);
            if (user == null)
            {
                throw new InvalidCredentialsException("Invalid credentials.");
            }

            if (!VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Incorrect password.");
            }
            string token = CreateToken(user);
            return token;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
