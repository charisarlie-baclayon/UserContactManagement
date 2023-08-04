using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos.CustomValidations
{
    public class AtLeastOneAddressRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addresses = value as List<AddressCreationDto>;

            if (addresses == null || addresses.Count == 0)
            {
                return new ValidationResult("At least one address is required.");
            }

            return ValidationResult.Success;
        }
    }
}
