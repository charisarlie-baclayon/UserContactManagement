using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Services.AuthenticationService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = ContactInformation.WebAPI.Services.AuthenticationService.IAuthenticationService;

namespace ContactInformation.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="authenticationService">The service for user authentication.</param>
        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userRegistrationDto">The user registration details.</param>
        /// <returns>The newly registered user.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /api/authentication/register
        ///     {
        ///         "username": "newUser",
        ///         "password": "password123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly registered user.</response>
        /// <response code="400">Invalid user registration details provided.</response>
        /// <response code="409">User already exists.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("register")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                var newUser = await _authenticationService.Register(userRegistrationDto);
                if (newUser == null)
                {
                    _logger.LogInformation($"Registration unsuccessful due to null return.");
                    return BadRequest();
                }
                return Ok(newUser);
            }
            catch (UserAlreadyExistsException ex)
            {
                _logger.LogError($"User already exists. Unsuccessful registration.");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userLoginDto">The user login details.</param>
        /// <returns>The authentication token upon successful login.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /api/authentication/login
        ///     {
        ///         "username": "existinguser",
        ///         "password": "password123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the authentication token upon successful login.</response>
        /// <response code="401">Unauthorized due to invalid credentials.</response>
        /// <response code="403">Unauthorized access.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationToken), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var token = await _authenticationService.Login(userLoginDto);
                if (token == null)
                {
                    _logger.LogInformation($"Login unsuccessful due to invalid credentials.");
                    return Unauthorized("Invalid credentials.");
                }

                return Ok(token);
            }
            catch (InvalidCredentialsException ex)
            {
                _logger.LogWarning($"Invalid credentials provided.");
                return Unauthorized(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning($"Unauthorized access.");
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
