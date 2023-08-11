using ContactInformation.WebAPI.Dtos.Contact;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.ContactService
{
    /// <summary>
    /// Represents a service that provides operations related to Contacts.
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Retrieves all contacts associated with the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose contacts to retrieve.</param>
        /// <returns>A collection of contact DTOs.</returns>
        Task<IEnumerable<ContactDto>> GetContacts(int userId);

        /// <summary>
        /// Retrieves a specific contact associated with the specified user and contact ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="contactId">The ID of the contact to retrieve.</param>
        /// <returns>The contact DTO.</returns>
        Task<ContactDto> GetContact(int userId, int contactId);

        /// <summary>
        /// Creates a new contact for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="contactToAdd">The contact data to create.</param>
        /// <returns>The ID of the newly created contact.</returns>
        Task<int> CreateContact(int userId, ContactCreationDto contactToAdd);

        /// <summary>
        /// Updates an existing contact for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="contactId">The ID of the contact to update.</param>
        /// <param name="contactToUpdate">The updated contact data.</param>
        /// <returns>The updated contact.</returns>
        Task<Contact> UpdateContact(int userId, int contactId, ContactUpdationDto contactToUpdate);

        /// <summary>
        /// Deletes a contact associated with the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="contactId">The ID of the contact to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteContact(int userId, int contactId);

    }
}
