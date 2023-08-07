using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/Contacts/{contactId}/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(ILogger<AddressesController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAddresses(int contactId)
        {
            try
            {
                var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contact == null)
                {
                    _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                    return NotFound();
                }
                var addresses = contact.Addresses!.ToList();
                if (addresses.Any())
                {
                    _logger.LogInformation($"Addresses were not found when accessing Contact with id {contactId}.");
                    return NotFound();
                }
                return Ok(addresses);
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
                var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contact == null)
                {
                    _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                    return NotFound();
                }
                var addressToReturn = contact.Addresses!.FirstOrDefault(a => a.Id == addressId);
                if (addressToReturn == null)
                {
                    _logger.LogInformation($"Address with id {addressId} was not found when accessing Contact with id {contactId}.");
                    return NotFound();
                }
                return Ok(addressToReturn);
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
                var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contact == null)
                {
                    _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                    return NotFound();
                }
                var maxAddressId = ContactsDataStore.Current.Contacts.SelectMany(c => c.Addresses!).Max(a => a.Id);

                var newAddress = new Address()
                {
                    Id = ++maxAddressId,
                    AddressDescription = addressToCreate.AddressDescription,
                    AddressType = addressToCreate.AddressType,
                };
                contact.Addresses!.Add(newAddress);

                return CreatedAtRoute("GetAddress", new { contactId = contactId, addressId = newAddress.Id }, newAddress);
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
                var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contact == null)
                {
                    _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                    return NotFound();
                }
                var addressFromStore = contact.Addresses!.FirstOrDefault(a => a.Id == addressId);
                if (addressFromStore == null)
                {
                    _logger.LogInformation($"Address with id {addressId} was not found when accessing Contact with id {contactId}.");
                    return NotFound();
                }

                addressFromStore.AddressDescription = addressToUpdate.AddressDescription;
                addressFromStore.AddressType = addressToUpdate.AddressType;

                return AcceptedAtRoute("GetAddress", new { contactId = contactId, addressId = addressFromStore.Id }, addressFromStore);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPatch("{addressId}")]
        public async Task<IActionResult> PartiallyUpdateAddress(int contactId, int addressId, JsonPatchDocument<AddressCreationDto> addressToUpdatePatchDocument)
        {
            try
            {
                var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contact == null)
                {
                    _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                    return NotFound();
                }

                var addressFromStore = contact.Addresses!.FirstOrDefault(a => a.Id == addressId);
                if (addressFromStore == null)
                {
                    _logger.LogInformation($"Address with id {addressId} was not found when accessing Contact with id {contactId}.");
                    return NotFound();
                }

                var addressToPatch = new AddressCreationDto()
                {
                    AddressDescription = addressFromStore.AddressDescription,
                    AddressType = addressFromStore.AddressType,
                };

                addressToUpdatePatchDocument.ApplyTo(addressToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!TryValidateModel(addressToPatch))
                {
                    return BadRequest(ModelState);
                }
                addressFromStore.AddressDescription = addressToPatch.AddressDescription;
                addressFromStore.AddressType = addressToPatch.AddressType;

                return AcceptedAtRoute("GetAddress", new { contactId = contactId, addressId = addressFromStore.Id }, addressFromStore);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int contactId, int addressId)
        {
            try
            {
                var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contact == null)
                {
                    _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                    return NotFound();
                }
                var addressFromStore = contact.Addresses!.FirstOrDefault(a => a.Id == addressId);
                if (addressFromStore == null)
                {
                    _logger.LogInformation($"Address with id {addressId} was not found when accessing Contact with id {contactId}.");
                    return NotFound();
                }
                contact.Addresses!.Remove(addressFromStore);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
