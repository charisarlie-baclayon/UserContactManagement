using ContactInformation.WebAPI.Dtos.Address;

namespace ContactInformation.WebAPI.Dtos.Contact
{
    /// <summary>
    /// Data transfer object for viewing contact details.
    /// </summary>
    public class ContactDto
    {
        /// <summary>
        /// Gets or sets the first name of the contact.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the contact.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the addresses of the contact.
        /// </summary>
        public List<AddressDto> Addresses { get; set; }
            = new List<AddressDto>();

        /// <summary>
        /// Gets or sets the phone number of the contact.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the contact.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the birth date of the contact.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets whether the contact is a favorite.
        /// </summary>
        public bool Favorite { get; set; }
    }
}
