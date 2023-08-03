﻿using ContactInformation.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(ContactsDataStore.Current.Contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contactToReturn = ContactsDataStore.Current.Contacts.FirstOrDefault(c => c.Id == id);
            if (contactToReturn == null)
            {
                return NotFound();
            }
            return Ok(contactToReturn);
        }
    }
}
