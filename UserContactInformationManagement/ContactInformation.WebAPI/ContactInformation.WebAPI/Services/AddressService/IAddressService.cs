using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.AddressService
{
    public interface IAddressService
    {
        Task<int> CreateAddress(int contactId, AddressCreationDto addressToAdd);
        Task<bool> DeleteAddress(int contactId, int addressId);
        Task<AddressDto> GetAddress(int contactId, int addressId);
        Task<IEnumerable<AddressDto>> GetAddresses(int contactId);
        Task<Address> UpdateAddress(int contactId, int addressId, AddressCreationDto addressToUpdate);
    }
}
