namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when provided credentials are invalid.
    /// </summary>
    public class InvalidCredentialsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidCredentialsException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidCredentialsException(string message) : base(message) { }
    }
}
