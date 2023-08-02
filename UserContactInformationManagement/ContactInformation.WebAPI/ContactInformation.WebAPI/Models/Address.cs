namespace ContactInformation.WebAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressDescription { get; set; }
        public string AddressType { get; set; }
        public Contact Contact { get; set; }
        public int ContactId { get; set; }
    }
}
