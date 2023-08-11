namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when a contact is not found.
    /// </summary>
    public class ContactNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ContactNotFoundException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ContactNotFoundException(string message) : base(message) { }
    }
}
