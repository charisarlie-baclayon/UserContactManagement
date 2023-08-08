using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories.AddressRepository
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddresses(int contactId);
        Task<Address> GetAddress(int contactId, int addressId);
        Task<int> CreateAddress(int contactId, Address addressToAdd);
        Task<Address> UpdateAddress(int contactId, Address addressToUpdate);
        Task<bool> DeleteAddress(int contactId, int addressId);
    }
}