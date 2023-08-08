using AutoMapper;
using ContactInformation.WebAPI.Dtos.Contact;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Mappings
{
    public class ContactMappings : Profile
    {
        public ContactMappings()
        {
            CreateMap<ContactCreationDto, Contact>();
            CreateMap<ContactDto, Contact>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));
            CreateMap<ContactUpdationDto, Contact>();
            CreateMap<Contact, ContactCreationDto>();
            CreateMap<Contact, ContactDto>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));
            CreateMap<Contact, ContactUpdationDto>();
        }
    }
}
