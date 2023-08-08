namespace ContactInformation.WebAPI.Exceptions
{
    public class ContactCreationFailedException : Exception
    {
        public ContactCreationFailedException(string message) : base(message) { }
    }
}
