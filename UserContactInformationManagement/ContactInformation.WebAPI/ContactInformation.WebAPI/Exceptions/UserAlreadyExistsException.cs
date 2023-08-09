namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when a user already exists.
    /// </summary>
    public class UserAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UserAlreadyExistsException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
