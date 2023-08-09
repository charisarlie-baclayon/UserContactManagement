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
    /// <summary>
    /// Controller responsible for managing contacts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactsController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="contactService">The service for managing contacts.</param>
        /// <param name="userService">The service for user-related operations.</param>
        public ContactsController(ILogger<ContactsController> logger, IContactService contactService, 
            IUserService userService)
        {
            _logger = logger;
            _contactService = contactService;
            _userService = userService;
        }

        /// <summary>
        /// Gets all contacts for the authenticated user.
        /// </summary>
        /// <returns>A list of contacts for the authenticated user.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/contacts
        ///
        /// </remarks>
        /// <response code="200">Returns a list of contacts for the authenticated user.</response>
        /// <response code="404">No contacts found for the authenticated user.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ContactDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var userId = await _userService.GetUserId();
                var contacts = await _contactService.GetContacts(userId);
                if (!contacts.Any())
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

        /// <summary>
        /// Gets a specific contact by its ID for the authenticated user.
        /// </summary>
        /// <param name="contactId">The ID of the contact to retrieve.</param>
        /// <returns>The requested contact for the authenticated user.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/contacts/{contactId}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested contact for the authenticated user.</response>
        /// <response code="404">Contact with the specified ID does not exist for the authenticated user.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{contactId}", Name = "GetContact")]
        [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Creates a new contact for the authenticated user.
        /// </summary>
        /// <param name="contactToCreate">The contact details to be created.</param>
        /// <returns>The newly created contact for the authenticated user.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /api/contacts
        ///     {
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "email": "john.doe@example.com"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created contact for the authenticated user.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactCreationDto contactToCreate)
        {
            try
            {
                var userId = await _userService.GetUserId();

                var contactId = await _contactService.CreateContact(userId, contactToCreate);
                //return CreatedAtRoute("GetContact", new { contactId }, null);
                return Ok(await _contactService.GetContact(userId, contactId));
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

        /// <summary>
        /// Updates a contact for the authenticated user.
        /// </summary>
        /// <param name="contactId">The ID of the contact to be updated.</param>
        /// <param name="contactToUpdate">The updated contact details.</param>
        /// <returns>The updated contact for the authenticated user.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     PUT /api/contacts/{contactId}
        ///     {
        ///         "firstName": "Updated John",
        ///         "lastName": "Updated Doe",
        ///         "email": "updated.john.doe@example.com"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the updated contact for the authenticated user.</response>
        /// <response code="404">Contact not found or contact update failed for the authenticated user.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{contactId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Deletes a contact for the authenticated user.
        /// </summary>
        /// <param name="contactId">The ID of the contact to be deleted.</param>
        /// <returns>Returns a status indicating the success of the operation.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     DELETE /api/contacts/{contactId}
        ///
        /// </remarks>
        /// <response code="200">Contact successfully deleted for the authenticated user.</response>
        /// <response code="404">Contact not found or contact deletion failed for the authenticated user.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{contactId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
