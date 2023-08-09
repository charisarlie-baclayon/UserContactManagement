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
    /// <summary>
    /// Provides method implementation from IAuthenticationService.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="mapper">AutoMapper instance for object mapping.</param>
        /// <param name="userService">User service for user-related operations.</param>
        /// <param name="configuration">Configuration settings.</param>
        public AuthenticationService(IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _mapper = mapper;
            _userService = userService;
            _configuration = configuration;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <summary>
        /// Creates a hash of the provided password and generates a salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordHash">The resulting password hash.</param>
        /// <param name="passwordSalt">The generated password salt.</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verifies whether the provided password matches the stored password hash.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="passwordHash">The stored password hash.</param>
        /// <param name="passwordSalt">The stored password salt.</param>
        /// <returns><c>true</c> if the password is verified; otherwise, <c>false</c>.</returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        /// <summary>
        /// Creates a JWT token for the provided user.
        /// </summary>
        /// <param name="user">The user for whom the token is created.</param>
        /// <returns>The generated JWT token.</returns>
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
