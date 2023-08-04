using ContactInformation.WebAPI.Dtos;
using ContactInformation.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/Contacts/{contactId}/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAddresses(int contactId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault( c => c.Id == contactId);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact.Addresses!.ToList());
        }

        [HttpGet("{addressId}", Name = "GetAddress")]
        public async Task<IActionResult> GetAddress(int contactId, int addressId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return NotFound();
            }
            var addressToReturn = contact.Addresses!.FirstOrDefault(a => a.Id == addressId);
            if (addressToReturn == null)
            {
                return NotFound();
            }
            return Ok(addressToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(int contactId, [FromBody] AddressCreationDto addressToCreate)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return NotFound();
            }
            var maxAddressId = ContactsDataStore.Current.Contacts.SelectMany(c => c.Addresses!).Max(a => a.Id);

            var newAddress = new Address()
            {
                Id = ++maxAddressId,
                AddressDescription = addressToCreate.AddressDescription,
                AddressType = addressToCreate.AddressType,
            };
            contact.Addresses.Add(newAddress);

            return CreatedAtRoute("GetAddress",new { contactId = contactId, addressId = newAddress.Id}, newAddress);
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int contactId, int addressId, [FromBody] AddressCreationDto addressToUpdate)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return NotFound();
            }
            var addressFromStore = contact.Addresses!.FirstOrDefault(a => a.Id == addressId);
            if (addressFromStore == null)
            {
                return NotFound();
            }

            addressFromStore.AddressDescription = addressToUpdate.AddressDescription;
            addressFromStore.AddressType = addressToUpdate.AddressType;

            //return NoContent();
            return AcceptedAtRoute("GetAddress", new { contactId = contactId, addressId = addressFromStore.Id }, addressFromStore);
        }

        [HttpPatch("{addressId}")]
        public async Task<IActionResult> PartiallyUpdateAddress(int contactId, int addressId, JsonPatchDocument<AddressCreationDto> addressToUpdatePatchDocument)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return NotFound();
            }

            var addressFromStore = contact.Addresses!.FirstOrDefault(a => a.Id == addressId);
            if (addressFromStore == null)
            {
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

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int contactId, int addressId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return NotFound();
            }
            var addressFromStore = contact.Addresses!.FirstOrDefault(a => a.Id == addressId);
            if (addressFromStore == null)
            {
                return NotFound();
            }
            contact.Addresses!.Remove(addressFromStore);
            return Ok();
        }
    }
}
