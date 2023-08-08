using System.Runtime.Serialization;

namespace ContactInformation.WebAPI.Exceptions
{
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException(string? message) : base(message)
        {
        }
    }
}