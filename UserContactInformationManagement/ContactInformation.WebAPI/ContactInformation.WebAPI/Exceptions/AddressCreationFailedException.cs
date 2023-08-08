using System.Runtime.Serialization;

namespace ContactInformation.WebAPI.Exceptions
{
    public class AddressCreationFailedException : Exception
    {
        public AddressCreationFailedException(string? message) : base(message)
        {
        }
    }
}