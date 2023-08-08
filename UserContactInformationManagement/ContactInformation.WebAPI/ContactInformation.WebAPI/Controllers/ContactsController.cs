﻿using ContactInformation.WebAPI.Dtos.Contact;
using ContactInformation.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    [Route("api/users/{userId}/contacts")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts(int userId)
        {
            try
            {
                var userContacts = ContactsDataStore.Current.Contacts
                    .Where(c => c.UserId == userId).ToList();
                if (userContacts.Any())
                {
                    _logger.LogInformation($"Contacts were not found when accessing Contacts.");
                    return NotFound();
                }
                return Ok(userContacts);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpGet("{contactId}", Name = "GetContact")]
        public async Task<IActionResult> GetContact(int userId, int contactId)
        {
            try
            {
                var contactToReturn = ContactsDataStore.Current.Contacts
                    .FirstOrDefault(c => c.UserId == userId && c.Id == contactId);
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
        public async Task<IActionResult> CreateContact(int userId, [FromBody] ContactCreationDto contactToCreate)
        {
            try
            {
                var userContacts = ContactsDataStore.Current.Contacts
                    .Where(c => c.UserId == userId).ToList();
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
                        var maxAddressId = ContactsDataStore.Current.Contacts
                            .SelectMany(c => c.Addresses!).Max(a => a.Id);
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

                return CreatedAtRoute("GetContact", new { userId = newContact.UserId, contactId = newContact.Id }, newContact);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
            
        }

        //need to clean
        [HttpPut("{contactId}")]
        public async Task<IActionResult> UpdateContact(int userId, int contactId, [FromBody] ContactUpdationDto contactToUpdate)
        {
            try
            {
                var contactFromStore = ContactsDataStore.Current.Contacts
                    .FirstOrDefault(c => c.UserId == userId && c.Id == contactId);
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
                        var existingAddress = contactFromStore.Addresses?
                            .FirstOrDefault(a => a.AddressDescription == addressUpdate.AddressDescription);
                        if (existingAddress != null)
                        {
                            // Update existing address properties
                            existingAddress.AddressDescription = addressUpdate.AddressDescription;
                            existingAddress.AddressType = addressUpdate.AddressType;
                        }
                        else
                        {
                            // Add new address if it doesn't exist
                            var maxAddressId = ContactsDataStore.Current.Contacts
                                .SelectMany(c => c.Addresses!).Max(a => a.Id);
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

                return AcceptedAtRoute("GetContact", new { userId = contactFromStore.UserId, contactId = contactFromStore.Id }, contactFromStore);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteContact(int userId, int contactId)
        {
            try
            {
                var contactToDelete = ContactsDataStore.Current.Contacts
                    .FirstOrDefault(c => c.UserId == userId && c.Id == contactId);
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
