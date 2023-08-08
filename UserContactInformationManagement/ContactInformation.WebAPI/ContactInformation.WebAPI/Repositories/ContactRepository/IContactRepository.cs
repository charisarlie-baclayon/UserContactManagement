using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories.ContactRepository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContacts(int userId);
        Task<Contact> GetContact(int userId, int contactId);
        Task<Contact> CreateContact(int userId, Contact contact);
        Task<Contact> UpdateContact(int userId, Contact contact);
        Task<bool> DeleteContact(int userId, int contactId);

    }
}
