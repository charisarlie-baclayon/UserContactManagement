using ContactInformation.WebAPI.Dtos.Address;

namespace ContactInformation.WebAPI.Dtos.Contact
{
    public class ContactDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<AddressDto> Addresses { get; set; }
            = new List<AddressDto>();
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public bool Favorite { get; set; }
    }
}
