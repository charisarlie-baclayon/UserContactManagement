using AutoMapper;
using ContactInformation.WebAPI.Dtos.Contact;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Mappings
{
    /// <summary>
    /// Provides mapping profiles for Contact related entities.
    /// </summary>
    public class ContactMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactMappings"/> class.
        /// Defines mapping configurations between Contact-related DTOs and entities.
        /// </summary>
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
