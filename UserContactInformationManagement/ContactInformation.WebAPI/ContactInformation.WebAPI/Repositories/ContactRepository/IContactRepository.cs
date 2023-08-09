using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories.ContactRepository
{
    /// <summary>
    /// Interface for defining contact-related data operations.
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Retrieves a list of contacts associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose contacts are to be retrieved.</param>
        /// <returns>A collection of contacts.</returns>
        Task<IEnumerable<Contact>> GetContacts(int userId);

        /// <summary>
        /// Retrieves a specific contact associated with a user.
        /// </summary>
        /// <param name="userId">The ID of the user whose contact is to be retrieved.</param>
        /// <param name="contactId">The ID of the contact to be retrieved.</param>
        /// <returns>The retrieved contact.</returns>
        Task<Contact> GetContact(int userId, int contactId);

        /// <summary>
        /// Creates a new contact for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the contact is to be created.</param>
        /// <param name="contactToAdd">The contact information to be added.</param>
        /// <returns>The ID of the created contact.</returns>
        Task<int> CreateContact(int userId, Contact contactToAdd);

        /// <summary>
        /// Updates an existing contact for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose contact is to be updated.</param>
        /// <param name="contactToUpdate">The updated contact information.</param>
        /// <returns>The updated contact.</returns>
        Task<Contact> UpdateContact(int userId, Contact contactToUpdate);

        /// <summary>
        /// Deletes a contact associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose contact is to be deleted.</param>
        /// <param name="contactId">The ID of the contact to be deleted.</param>
        /// <returns>True if the contact is deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteContact(int userId, int contactId);

    }
}
