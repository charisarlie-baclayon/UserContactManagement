namespace ContactInformation.WebAPI.Dtos.Address
{
    /// <summary>
    /// Data transfer object for viewing address details.
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Gets or sets the description of the address.
        /// </summary>
        public string AddressDescription { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the address.
        /// </summary>
        public string AddressType { get; set; } = string.Empty;
    }
}
