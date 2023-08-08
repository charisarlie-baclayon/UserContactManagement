namespace ContactInformation.WebAPI.Exceptions
{
    public class UserUpdateFailedException : Exception
    {
        public UserUpdateFailedException(string message) : base(message) { }
    }
}
