using AutoMapper;
using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Repositories.AddressRepository;

namespace ContactInformation.WebAPI.Services.AddressService
{
    /// <summary>
    /// Provides method implementation from IAddressService.
    /// </summary>
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressService"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance for mapping between entities and DTOs.</param>
        /// <param name="addressRepository">The repository for address data operations.</param>
        public AddressService(IMapper mapper, IAddressRepository addressRepository)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
        }

        /// <inheritdoc />
        public async Task<int> CreateAddress(int contactId, AddressCreationDto addressToAdd)
        {
            var newAddress = _mapper.Map<Address>(addressToAdd);
            newAddress.ContactId = contactId;

            var addressId = await _addressRepository.CreateAddress(contactId, newAddress);
            if (addressId == 0)
            {
                throw new AddressCreationFailedException("Address creation failed.");
            }
            return addressId;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAddress(int contactId, int addressId)
        {
            var deleted = await _addressRepository.DeleteAddress(contactId, addressId);
            if (!deleted)
            {
                throw new AddressDeletionFailedException("Address deletion failed.");
            }
            return deleted;
        }

        /// <inheritdoc />
        public async Task<AddressDto> GetAddress(int contactId, int addressId)
        {
            var address = await _addressRepository.GetAddress(contactId, addressId);
            if (address == null)
            {
                throw new AddressNotFoundException($"Address with ID {addressId} not found.");
            }
            var newAddressDto = _mapper.Map<AddressDto>(address);
            return newAddressDto;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AddressDto>> GetAddresses(int contactId)
        {
            var addresses = await _addressRepository.GetAddresses(contactId);
            var addressDtos = _mapper.Map<IEnumerable<AddressDto>>(addresses);
            return addressDtos;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AddressType>> GetAddressTypes()
        {
            var addressTypes = Enum.GetValues(typeof(AddressType));
            return await Task.FromResult(addressTypes.Cast<AddressType>().ToList());
        }

        /// <inheritdoc />
        public async Task<Address> UpdateAddress(int contactId, int addressId, AddressCreationDto addressToUpdate)
        {
            var existingAddress = await _addressRepository.GetAddress(contactId, addressId);
            if (existingAddress == null)
            {
                throw new AddressNotFoundException($"Address with ID {addressId} not found.");
            }

            var updatedAddress = _mapper.Map(addressToUpdate, existingAddress);

            var result = await _addressRepository.UpdateAddress(contactId, updatedAddress);
            if (result == null)
            {
                throw new AddressUpdateFailedException("Address update failed.");
            }
            return result!;
        }
    }
}
