using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Dtos
{
    public class ContactCreationDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Address>? Addresses { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public bool Favorite { get; set; }
    }
}
