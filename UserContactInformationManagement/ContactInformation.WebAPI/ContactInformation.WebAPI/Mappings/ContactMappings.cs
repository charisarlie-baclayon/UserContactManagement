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
            CreateMap<ContactDto, Contact>();
            CreateMap<ContactUpdationDto, Contact>();
            CreateMap<Contact, ContactCreationDto>();
            CreateMap<Contact, ContactDto>();
            CreateMap<Contact, ContactUpdationDto>();
        }
    }
}
