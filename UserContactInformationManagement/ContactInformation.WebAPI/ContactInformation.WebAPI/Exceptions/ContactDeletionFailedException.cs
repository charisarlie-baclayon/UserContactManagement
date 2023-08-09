namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when the deletion of a contact fails.
    /// </summary>
    public class ContactDeletionFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ContactDeletionFailedException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ContactDeletionFailedException(string message) : base(message) { }
    }
}
