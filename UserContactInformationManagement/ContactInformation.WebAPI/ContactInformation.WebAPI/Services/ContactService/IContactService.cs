using ContactInformation.WebAPI.Dtos.Contact;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.ContactService
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetContacts(int userId);
        Task<ContactDto> GetContact(int userId, int contactId);
        Task<int> CreateContact(int userId, ContactCreationDto contactToAdd);
        Task<Contact> UpdateContact(int userId, int contactId, ContactUpdationDto contactToUpdate);
        Task<bool> DeleteContact(int userId, int contactId);
    }
}
