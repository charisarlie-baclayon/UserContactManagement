using System.Runtime.Serialization;

namespace ContactInformation.WebAPI.Exceptions
{
    public class AddressUpdateFailedException : Exception
    {
        public AddressUpdateFailedException(string? message) : base(message)
        {
        }
    }
}