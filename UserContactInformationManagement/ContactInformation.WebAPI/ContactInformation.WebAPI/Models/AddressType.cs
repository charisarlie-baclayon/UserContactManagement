namespace ContactInformation.WebAPI.Models
{
    /// <summary>
    /// Represents the types of addresses.
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// Represents a delivery address type.
        /// </summary>
        Delivery,

        /// <summary>
        /// Represents a billing address type.
        /// </summary>
        Billing,

        /// <summary>
        /// Represents a home address type.
        /// </summary>
        Home,

        /// <summary>
        /// Represents a work address type.
        /// </summary>
        Work
    }
}
