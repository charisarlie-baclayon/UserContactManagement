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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private int _userId;

        public UsersController(ILogger<UsersController> logger, IContactService contactService,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            GetUserIdentity();
        }

        private async void GetUserIdentity()
        {
            _userId = await _userService.GetUserId();
        }

        [HttpGet]
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

        [HttpPut]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                var deleted = await _userService.DeleteUser(_userId);
                return NotFound();
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
