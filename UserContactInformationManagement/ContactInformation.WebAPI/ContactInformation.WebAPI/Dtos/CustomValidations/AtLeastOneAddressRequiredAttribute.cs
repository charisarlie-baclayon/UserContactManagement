using System.ComponentModel.DataAnnotations;
using ContactInformation.WebAPI.Dtos.Address;

namespace ContactInformation.WebAPI.Dtos.CustomValidations
{
    /// <summary>
    /// Validates that at least one address is required in a list of address creation DTOs.
    /// </summary>
    public class AtLeastOneAddressRequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates that at least one address is required in the list.
        /// </summary>
        /// <param name="value">The value being validated (should be a list of address creation DTOs).</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A validation result indicating success or failure with an error message.</returns>
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is List<AddressCreationDto> addresses && addresses.Count > 0)
            {
                return ValidationResult.Success!;
            }

            return new ValidationResult("At least one address is required.");
        }
    }
}
