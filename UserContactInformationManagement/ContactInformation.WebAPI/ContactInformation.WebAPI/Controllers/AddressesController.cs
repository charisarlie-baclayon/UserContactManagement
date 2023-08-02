using ContactInformation.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/contacts/{contactId}/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses(int contactId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault( c => c.Id == contactId);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact.Addresses.ToList());
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<Address>> GetAddress(int contactId, int addressId)
        {
            var contact = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return NotFound();
            }
            var addressToReturn = contact.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (addressToReturn == null)
            {
                return NotFound();
            }
            return Ok(addressToReturn);
        }
    }
}
