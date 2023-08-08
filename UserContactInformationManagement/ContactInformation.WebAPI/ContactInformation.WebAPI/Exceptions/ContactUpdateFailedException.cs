namespace ContactInformation.WebAPI.Exceptions
{
    public class ContactUpdateFailedException : Exception
    {
        public ContactUpdateFailedException(string message) : base(message) { }
    }
}
