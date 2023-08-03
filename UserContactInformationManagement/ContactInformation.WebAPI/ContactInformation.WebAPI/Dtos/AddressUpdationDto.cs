using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos
{
    public class AddressUpdationDto
    {
        [Required(ErrorMessage = "Address is required.")]
        public string AddressDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address Type is required.")]
        public string AddressType { get; set; } = string.Empty;
    }
}
