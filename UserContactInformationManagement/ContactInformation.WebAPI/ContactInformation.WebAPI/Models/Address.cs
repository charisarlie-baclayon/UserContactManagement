namespace ContactInformation.WebAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressDescription { get; set; } = string.Empty;
        public string AddressType { get; set; } = string.Empty;
        public Contact? Contact { get; set; }
        public int ContactId { get; set; }
    }
}
