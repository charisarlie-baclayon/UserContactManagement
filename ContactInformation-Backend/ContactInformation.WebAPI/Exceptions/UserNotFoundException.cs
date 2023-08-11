namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when a user is not found.
    /// </summary>
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UserNotFoundException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UserNotFoundException(string message) : base(message) { }
    }
}
