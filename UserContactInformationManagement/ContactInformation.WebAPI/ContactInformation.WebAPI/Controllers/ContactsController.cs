using ContactInformation.WebAPI.Dtos.Contact;
using ContactInformation.WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contactsToReturn = ContactsDataStore.Current.Contacts;
                if (contactsToReturn.Any())
                {
                    _logger.LogInformation($"Contacts were not found when accessing Contacts.");
                    return NotFound();
                }
                return Ok(contactsToReturn);
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
                var contactToReturn = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contactToReturn == null)
                {
                    _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                    return NotFound();
                }
                return Ok(contactToReturn);
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
                var maxContactId = ContactsDataStore.Current.Contacts.Max(c => c.Id);

                var newContact = new Contact()
                {
                    Id = ++maxContactId,
                    FirstName = contactToCreate.FirstName,
                    LastName = contactToCreate.LastName,
                    Addresses = new List<Address>(),
                    PhoneNumber = contactToCreate.PhoneNumber,
                    EmailAddress = contactToCreate.EmailAddress,
                    BirthDate = contactToCreate.BirthDate
                };

                if (contactToCreate.Addresses != null)
                {
                    foreach (var address in contactToCreate.Addresses)
                    {
                        var maxAddressId = ContactsDataStore.Current.Contacts.SelectMany(c => c.Addresses!).Max(a => a.Id);
                        var newAddress = new Address()
                        {
                            Id = ++maxAddressId,
                            AddressDescription = address.AddressDescription,
                            AddressType = address.AddressType
                        };
                        newContact.Addresses.Add(newAddress);
                    }
                }
                ContactsDataStore.Current.Contacts.Add(newContact);

                return CreatedAtRoute("GetContact", new { contactId = newContact }, newContact);
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
                var contactFromStore = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contactFromStore == null)
                {
                    return NotFound();
                }

                contactFromStore.FirstName = contactToUpdate.FirstName;
                contactFromStore.LastName = contactToUpdate.LastName;
                contactFromStore.PhoneNumber = contactToUpdate.PhoneNumber;
                contactFromStore.EmailAddress = contactToUpdate.EmailAddress;
                contactFromStore.BirthDate = contactToUpdate.BirthDate;
                contactFromStore.Favorite = contactToUpdate.Favorite;

                if (contactToUpdate.Addresses != null)
                {
                    foreach (var addressUpdate in contactToUpdate.Addresses)
                    {
                        var existingAddress = contactFromStore.Addresses?.FirstOrDefault(a => a.AddressDescription == addressUpdate.AddressDescription);
                        if (existingAddress != null)
                        {
                            // Update existing address properties
                            existingAddress.AddressDescription = addressUpdate.AddressDescription;
                            existingAddress.AddressType = addressUpdate.AddressType;
                        }
                        else
                        {
                            // Add new address if it doesn't exist
                            var maxAddressId = ContactsDataStore.Current.Contacts.SelectMany(c => c.Addresses!).Max(a => a.Id);
                            var newAddress = new Address()
                            {
                                Id = ++maxAddressId,
                                AddressDescription = addressUpdate.AddressDescription,
                                AddressType = addressUpdate.AddressType
                            };
                            contactFromStore.Addresses!.Add(newAddress);
                        }
                    }
                }

                return AcceptedAtRoute("GetContact", new { contactId = contactFromStore.Id }, contactFromStore);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteCopntact(int contactId)
        {
            try
            {
                var contactToDelete = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (contactToDelete == null)
                {
                    _logger.LogInformation($"Contact with id {contactId} was not found when accessing Contacts.");
                    return NotFound();
                }
                ContactsDataStore.Current.Contacts.Remove(contactToDelete);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}
