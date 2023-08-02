namespace ContactInformation.WebAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Favorite { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
