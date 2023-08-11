namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when the creation of a contact fails.
    /// </summary>
    public class ContactCreationFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ContactCreationFailedException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ContactCreationFailedException(string message) : base(message) { }
    }
}
