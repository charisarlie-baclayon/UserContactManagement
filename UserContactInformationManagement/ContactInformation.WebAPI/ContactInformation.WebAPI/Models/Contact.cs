namespace ContactInformation.WebAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Address>? Addresses { get; set; }    
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public bool Favorite { get; set; }
        public int UserId { get; set; }

    }
}
