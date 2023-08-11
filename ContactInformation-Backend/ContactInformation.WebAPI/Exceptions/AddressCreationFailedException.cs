namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when the creation of an address fails.
    /// </summary>
    [Serializable]
    public class AddressCreationFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the AddressCreationFailedException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AddressCreationFailedException(string? message) : base(message)
        {
        }
    }
}
