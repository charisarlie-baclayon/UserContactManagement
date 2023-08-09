using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.AddressService
{
    /// <summary>
    /// Service interface for address operations.
    /// </summary>
    public interface IAddressService
    {
        /// <summary>
        /// Creates a new address for the specified contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact for which the address is created.</param>
        /// <param name="addressToAdd">The address data to be added.</param>
        /// <returns>The ID of the newly created address.</returns>
        Task<int> CreateAddress(int contactId, AddressCreationDto addressToAdd);

        /// <summary>
        /// Deletes the specified address associated with a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact that the address belongs to.</param>
        /// <param name="addressId">The ID of the address to be deleted.</param>
        /// <returns>True if the address was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteAddress(int contactId, int addressId);

        /// <summary>
        /// Retrieves a specific address associated with a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact that the address belongs to.</param>
        /// <param name="addressId">The ID of the address to retrieve.</param>
        /// <returns>The retrieved address DTO.</returns>
        Task<AddressDto> GetAddress(int contactId, int addressId);

        /// <summary>
        /// Retrieves all addresses associated with a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact for which to retrieve addresses.</param>
        /// <returns>An enumerable collection of address DTOs.</returns>
        Task<IEnumerable<AddressDto>> GetAddresses(int contactId);

        /// <summary>
        /// Updates a specific address associated with a contact.
        /// </summary>
        /// <param name="contactId">The ID of the contact that the address belongs to.</param>
        /// <param name="addressId">The ID of the address to update.</param>
        /// <param name="addressToUpdate">The updated address data.</param>
        /// <returns>The updated address model.</returns>
        Task<Address> UpdateAddress(int contactId, int addressId, AddressCreationDto addressToUpdate);

        /// <summary>
        /// Gets a list of address types.
        /// </summary>
        /// <returns>A list of address types.</returns>
        Task<IEnumerable<string>> GetAddressTypes();
    }
}
