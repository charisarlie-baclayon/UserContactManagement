using AutoMapper;
using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Mappings
{
    public class AddressMappings : Profile
    {
        public AddressMappings()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<Address, AddressCreationDto>();
            CreateMap<AddressCreationDto, Address>();
            CreateMap<AddressDto, Address>();
        }
    }
}
