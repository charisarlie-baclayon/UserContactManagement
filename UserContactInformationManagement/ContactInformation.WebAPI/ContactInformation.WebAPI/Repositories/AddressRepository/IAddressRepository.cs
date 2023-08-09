using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories.AddressRepository
{
    /// <summary>
    /// Interface for performing address-related data operations.
    /// </summary>
    public interface IAddressRepository
    {
        /// <summary>
        /// Gets a collection of addresses associated with a specific contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <returns>A collection of addresses.</returns>
        Task<IEnumerable<Address>> GetAddresses(int contactId);

        /// <summary>
        /// Gets a specific address associated with a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <param name="addressId">The ID of the address.</param>
        /// <returns>The retrieved address.</returns>
        Task<Address> GetAddress(int contactId, int addressId);

        /// <summary>
        /// Creates a new address associated with a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <param name="addressToAdd">The address to be added.</param>
        /// <returns>The ID of the newly created address.</returns>
        Task<int> CreateAddress(int contactId, Address addressToAdd);

        /// <summary>
        /// Updates an existing address associated with a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <param name="addressToUpdate">The updated address information.</param>
        /// <returns>The updated address.</returns>
        Task<Address> UpdateAddress(int contactId, Address addressToUpdate);

        /// <summary>
        /// Deletes a specific address associated with a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact.</param>
        /// <param name="addressId">The ID of the address to be deleted.</param>
        /// <returns>A boolean indicating the success of the deletion operation.</returns>
        Task<bool> DeleteAddress(int contactId, int addressId);
    }
}