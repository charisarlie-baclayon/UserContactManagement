namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when an address is not found.
    /// </summary>
    [Serializable]
    public class AddressNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the AddressNotFoundException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AddressNotFoundException(string? message) : base(message)
        {
        }
    }
}
