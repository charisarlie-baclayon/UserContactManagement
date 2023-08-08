namespace ContactInformation.WebAPI.Exceptions
{
    public class ContactDeletionFailedException : Exception
    {
        public ContactDeletionFailedException(string message) : base(message) { }
    }
}
