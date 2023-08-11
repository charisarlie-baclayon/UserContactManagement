using ContactInformation.WebAPI.Dtos.User;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Services.ContactService;
using ContactInformation.WebAPI.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for managing user-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private int _userId;

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="userService">The service for user-related operations.</param>
        public UsersController(ILogger<UsersController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            GetUserIdentity();
        }

        /// <summary>
        /// Retrieves the user identity asynchronously and assigns it to the _userId field.
        /// </summary>
        private async void GetUserIdentity()
        {
            _userId = await _userService.GetUserId();
        }

        /// <summary>
        /// Gets the details of the authenticated user.
        /// </summary>
        /// <returns>The details of the authenticated user.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/user
        ///
        /// </remarks>
        /// <response code="200">Returns the details of the authenticated user.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var user = await _userService.GetUserById(_userId);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        /// <summary>
        /// Updates the details of the authenticated user.
        /// </summary>
        /// <param name="userToUpdate">The updated user details.</param>
        /// <returns>The updated user details.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     PUT /api/user
        ///     {
        ///         "firstName": "UpdatedFirstName",
        ///         "lastName": "UpdatedLastName",
        ///         "username": "newusername",
        ///         "password": "newpassword",
        ///         "confirmPassword": "newpassword"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the updated user details.</response>
        /// <response code="404">User not found or update failed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(UserRegistrationDto userToUpdate)
        {
            try
            {
                var updatedUserDto = await _userService.UpdateUser(_userId, userToUpdate);
                return Ok(updatedUserDto);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (UserUpdateFailedException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "User update failed.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        /// <summary>
        /// Deletes the authenticated user.
        /// </summary>
        /// <returns>Returns a status indicating the success of the operation.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     DELETE /api/user
        ///
        /// </remarks>
        /// <response code="200">User successfully deleted.</response>
        /// <response code="404">User not found or deletion failed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                var deleted = await _userService.DeleteUser(_userId);
                return Ok();
            }
            catch (UserDeletionFailedException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message); 
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong."); 
            }
        }
    }
}
