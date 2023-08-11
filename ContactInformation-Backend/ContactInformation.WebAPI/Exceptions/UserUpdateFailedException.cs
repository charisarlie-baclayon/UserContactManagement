namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when a user update operation fails.
    /// </summary>
    public class UserUpdateFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UserUpdateFailedException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UserUpdateFailedException(string message) : base(message) { }
    }
}
