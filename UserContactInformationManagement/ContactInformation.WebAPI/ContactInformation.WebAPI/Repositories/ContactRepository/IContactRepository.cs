using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories.ContactRepository
{
    public interface IContactRepository
    {
        //Task<User> GetUserByToken(string token);
        Task<IEnumerable<Contact>> GetContacts(int userId);
        Task<Contact> GetContact(int userId, int contactId);
        Task<int> CreateContact(int userId, Contact contactToAdd);
        Task<Contact> UpdateContact(int userId, Contact contactToUpdate);
        Task<bool> DeleteContact(int userId, int contactId);

    }
}
