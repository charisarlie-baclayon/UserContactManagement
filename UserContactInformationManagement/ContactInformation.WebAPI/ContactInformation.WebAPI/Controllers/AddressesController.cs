using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Services.AddressService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;
        private readonly IAddressService _addressService;

        public AddressesController(ILogger<AddressesController> logger, IAddressService addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }
        [HttpGet]
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

        [HttpGet("{addressId}", Name = "GetAddress")]
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

        [HttpPost]
        public async Task<IActionResult> CreateAddress(int contactId, [FromBody] AddressCreationDto addressToCreate)
        {
            try
            {
                var addressId = await _addressService.CreateAddress(contactId, addressToCreate);
                //return CreatedAtRoute("GetAddress", new { contactId = contactId, addressId = addressId }, null);
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

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int contactId, int addressId, [FromBody] AddressCreationDto addressToUpdate)
        {
            try
            {
                var updatedAddress = await _addressService.UpdateAddress(contactId, addressId, addressToUpdate);
                //return AcceptedAtRoute("GetAddress", new { contactId = contactId, addressId = updatedAddress.Id }, updatedAddress);
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
        
        [HttpDelete("{addressId}")]
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

        [HttpGet]
        public async Task<IActionResult> GetAddressTypes()
        {
            try
            {
                var addressTypes = await _addressService.GetAddressTypes();
                return Ok(addressTypes);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, "An error occurred while fetching address types.");
            }
        }
    }
}
