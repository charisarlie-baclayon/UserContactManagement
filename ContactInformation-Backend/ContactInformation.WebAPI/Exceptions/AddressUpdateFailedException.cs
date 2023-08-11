namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when the update of an address fails.
    /// </summary>
    [Serializable]
    public class AddressUpdateFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the AddressUpdateFailedException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AddressUpdateFailedException(string? message) : base(message)
        {
        }
    }
}
