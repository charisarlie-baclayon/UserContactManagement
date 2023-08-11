using AutoMapper;
using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Mappings
{
    /// <summary>
    /// Provides mapping profiles for Address related entities.
    /// </summary>
    public class AddressMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressMappings"/> class.
        /// Defines mapping configurations between Address-related DTOs and entities.
        /// </summary>
        public AddressMappings()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<Address, AddressCreationDto>();
            CreateMap<AddressCreationDto, Address>();
            CreateMap<AddressDto, Address>();
        }
    }
}
