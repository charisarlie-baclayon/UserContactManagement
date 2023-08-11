using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Dtos.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos.Contact
{
    /// <summary>
    /// Data transfer object for updating contact details.
    /// </summary>
    public class ContactUpdationDto
    {
        /// <summary>
        /// Gets or sets the first name of the contact.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the contact.
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        ///// <summary>
        ///// Gets or sets the addresses of the contact.
        ///// </summary>
        //[Required(ErrorMessage = "At least one address is required.")]
        //[AtLeastOneAddressRequired(ErrorMessage = "At least one address is required.")]
        //public List<AddressCreationDto>? Addresses { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the contact.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be an 11-digit number.")]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the contact.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the birth date of the contact.
        /// </summary>
        [Required(ErrorMessage = "Birth date is required.")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets whether the contact is a favorite.
        /// </summary>
        public bool Favorite { get; set; }
    }
}
