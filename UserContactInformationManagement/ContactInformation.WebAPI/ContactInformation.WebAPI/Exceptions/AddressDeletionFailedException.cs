namespace ContactInformation.WebAPI.Exceptions
{
    /// <summary>
    /// Exception class used when the deletion of an address fails.
    /// </summary>
    [Serializable]
    public class AddressDeletionFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the AddressDeletionFailedException class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AddressDeletionFailedException(string message) : base(message)
        {
        }
    }
}
