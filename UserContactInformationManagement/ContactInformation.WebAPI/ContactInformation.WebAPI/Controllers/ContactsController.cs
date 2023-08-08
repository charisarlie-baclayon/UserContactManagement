using ContactInformation.WebAPI.Dtos.Contact;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Services.ContactService;
using ContactInformation.WebAPI.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public ContactsController(ILogger<ContactsController> logger, IContactService contactService, 
            IUserService userService)
        {
            _logger = logger;
            _contactService = contactService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var userId = await _userService.GetUserId();
                var contacts = await _contactService.GetContacts(userId);
                if (contacts.Any())
                {
                    _logger.LogInformation($"Contacts were not found when accessing Contacts.");
                    return NotFound();
                }
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpGet("{contactId}", Name = "GetContact")]
        public async Task<IActionResult> GetContact(int contactId)
        {
            try
            {
                var userId = await _userService.GetUserId();
                var contact = await _contactService.GetContact(userId, contactId);
                return Ok(contact);
            }
            catch (ContactNotFoundException ex)
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

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactCreationDto contactToCreate)
        {
            try
            {
                var userId = await _userService.GetUserId();

                var contactId = await _contactService.CreateContact(userId, contactToCreate);
                return CreatedAtRoute("GetContact", new { contactId }, null);
            }
            catch (ContactCreationFailedException ex)
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

        //need to clean
        [HttpPut("{contactId}")]
        public async Task<IActionResult> UpdateContact(int contactId, [FromBody] ContactUpdationDto contactToUpdate)
        {
            try
            {
                var userId = await _userService.GetUserId();

                var updatedContact = await _contactService.UpdateContact(userId, contactId, contactToUpdate);
                return Ok(updatedContact);

            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (ContactUpdateFailedException ex)
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

        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            try
            {
                var userId = await _userService.GetUserId();
                var deleted = await _contactService.DeleteContact(userId, contactId);
                if (deleted)
                {
                    return Ok();
                }
                _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                return NotFound();
            }
            catch (ContactDeletionFailedException ex)
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
