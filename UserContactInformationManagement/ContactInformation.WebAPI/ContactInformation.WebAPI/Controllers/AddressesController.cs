using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Services.AddressService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for managing addresses of contacts.
    /// </summary>
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;
        private readonly IAddressService _addressService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressesController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="addressService">The service for managing addresses.</param>
        public AddressesController(ILogger<AddressesController> logger, IAddressService addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        /// <summary>
        /// Gets all addresses for a specific contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact whose addresses are to be retrieved.</param>
        /// <returns>A list of addresses for the specified contact.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/contacts/{contactId}/addresses
        ///
        /// </remarks>
        /// <response code="200">Returns a list of addresses for the contact.</response>
        /// <response code="404">Contact with the specified ID does not exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<AddressDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddresses(int contactId)
        {
            try
            {
                var addresses = await _addressService.GetAddresses(contactId);
                return Ok(addresses);
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }


        /// <summary>
        /// Gets a specific address for a contact by its ID.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <param name="addressId">The ID of the address to retrieve.</param>
        /// <returns>The requested address for the contact.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/contacts/{contactId}/addresses/{addressId}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested address for the contact.</response>
        /// <response code="404">Contact or address with the specified IDs does not exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{addressId}", Name = "GetAddress")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddress(int contactId, int addressId)
        {
            try
            {
                var address = await _addressService.GetAddress(contactId, addressId);
                return Ok(address);
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
            catch (AddressNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }


        /// <summary>
        /// Creates a new address for a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <param name="addressToCreate">The address details to be created.</param>
        /// <returns>The newly created address for the contact.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /api/contacts/{contactId}/addresses
        ///     {
        ///         "addressDescription": "V. Rama Avenue, Guadalupe, Cebu City, Cebu",
        ///         "addressType": "Home"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created address for the contact.</response>
        /// <response code="400">Invalid address details provided.</response>
        /// <response code="404">Contact not found or failed to create address.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAddress(int contactId, [FromBody] AddressCreationDto addressToCreate)
        {
            try
            {
                var addressId = await _addressService.CreateAddress(contactId, addressToCreate);
                return Ok(await _addressService.GetAddress(contactId, addressId));
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
            catch (AddressCreationFailedException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }


        /// <summary>
        /// Updates an address for a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <param name="addressId">The ID of the address to be updated.</param>
        /// <param name="addressToUpdate">The updated address details.</param>
        /// <returns>The updated address for the contact.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     PUT /api/contacts/{contactId}/addresses/{addressId}
        ///     {
        ///         "addressDescription": "456 Oak St",
        ///         "addressType": "Work"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the updated address for the contact.</response>
        /// <response code="400">Invalid address details provided.</response>
        /// <response code="404">Contact or address not found, or address update failed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{addressId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddress(int contactId, int addressId, [FromBody] AddressCreationDto addressToUpdate)
        {
            try
            {
                var updatedAddress = await _addressService.UpdateAddress(contactId, addressId, addressToUpdate);
                return Ok(await _addressService.GetAddress(contactId, addressId));
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
            catch (AddressNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
            catch (AddressUpdateFailedException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }


        /// <summary>
        /// Deletes an address for a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <param name="addressId">The ID of the address to be deleted.</param>
        /// <returns>Returns a status indicating the success of the operation.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     DELETE /api/contacts/{contactId}/addresses/{addressId}
        ///
        /// </remarks>
        /// <response code="200">Address successfully deleted.</response>
        /// <response code="404">Contact or address not found, or address deletion failed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{addressId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAddress(int contactId, int addressId)
        {
            try
            {
                var deleted = await _addressService.DeleteAddress(contactId, addressId);
                if (deleted)
                {
                    return Ok();
                }
                _logger.LogInformation($"Address with id {addressId} cannot be found in this contact.");
                return NotFound();
            }
            catch (ContactNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
            catch (AddressDeletionFailedException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }


        /// <summary>
        /// Gets the available address types.
        /// </summary>
        /// <returns>Returns a list of available address types.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/contacts/{contactId}/addresses/address-types
        ///
        /// </remarks>
        /// <response code="200">Successfully retrieves the list of address types.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("address-types")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressTypes()
        {
            try
            {
                var addressTypes = await _addressService.GetAddressTypes();
                return Ok(addressTypes);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "An error occurred while fetching address types.");
            }
        }

    }
}
