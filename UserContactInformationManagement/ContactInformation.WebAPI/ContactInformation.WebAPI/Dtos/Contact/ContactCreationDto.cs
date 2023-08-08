using ContactInformation.WebAPI.Dtos.Address;
using ContactInformation.WebAPI.Dtos.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos.Contact
{
    public class ContactCreationDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "At least one address is required.")]
        [AtLeastOneAddressRequired(ErrorMessage = "At least one address is required.")]
        public List<AddressCreationDto>? Addresses { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be an 11-digit number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth date is required.")]
        public DateTime BirthDate { get; set; }
    }
}
