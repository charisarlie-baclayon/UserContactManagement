using ContactInformation.WebAPI.Context;
using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactInformation.WebAPI.Repositories.ContactRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactInformationDbContext _context;

        public ContactRepository(ContactInformationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetContacts(int userId)
        {
            var contacts = await _context.Contacts
                .Where(c => c.UserId == userId).ToListAsync();
            return contacts;
        }

        public async Task<Contact> GetContact(int userId, int contactId)
        {
            var contact = await _context.Contacts
                .FirstOrDefault(c => c.UserId == userId && c.Id == contactId);
            return contact;
        }

        public async Task<Contact> CreateContact(int userId, Contact contact)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteContact(int userId, int contactId)
        {
            throw new NotImplementedException();
        }

        public async Task<Contact> UpdateContact(int userId, Contact contact)
        {
            throw new NotImplementedException();
        }

    }
}
