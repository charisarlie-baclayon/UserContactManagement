namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when a contact update operation fails.
    /// </summary>
    public class ContactUpdateFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ContactUpdateFailedException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ContactUpdateFailedException(string message) : base(message) { }
    }
}
