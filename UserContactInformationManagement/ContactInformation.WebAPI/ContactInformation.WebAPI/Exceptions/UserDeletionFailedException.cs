namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when user deletion fails.
    /// </summary>
    public class UserDeletionFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UserDeletionFailedException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UserDeletionFailedException(string? message) : base(message)
        {
        }
    }
}
