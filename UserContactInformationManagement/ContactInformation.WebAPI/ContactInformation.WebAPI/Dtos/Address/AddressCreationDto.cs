using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos.Address
{
    /// <summary>
    /// Data transfer object for creating a new address.
    /// </summary>
    public class AddressCreationDto
    {
        /// <summary>
        /// Gets or sets the description of the address.
        /// </summary>
        [Required(ErrorMessage = "Address is required.")]
        public string AddressDescription { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the address.
        /// </summary>
        [Required(ErrorMessage = "Address Type is required.")]
        public string AddressType { get; set; } = string.Empty;
    }
}
