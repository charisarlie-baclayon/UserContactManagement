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

        //public async Task<User> GetUserByToken(string token)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(
        //        (e) => e.Username.Equals(token));
        //    return user!;
        //}

        public async Task<IEnumerable<Contact>> GetContacts(int userId)
        {
            var contacts = await _context.Contacts
                .Where(c => c.UserId == userId).ToListAsync();
            return contacts;
        }

        public async Task<Contact> GetContact(int userId, int contactId)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Id == contactId);
            return contact!;
        }

        public async Task<int> CreateContact(int userId, Contact contactToAdd)
        {
            _context.Contacts.Add(contactToAdd);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return contactToAdd.Id;
            }
            return 0;
        }

        public async Task<Contact> UpdateContact(int userId, Contact contactToUpdate)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Id == contactToUpdate.Id);
            if (contact == null)
            {
                return null!;
            }
            contact!.FirstName = contactToUpdate.FirstName;
            contact.LastName = contactToUpdate.LastName;
            contact.PhoneNumber = contactToUpdate.PhoneNumber;
            contact.EmailAddress = contactToUpdate.EmailAddress;
            contact.BirthDate = contactToUpdate.BirthDate;
            contact.Favorite = contactToUpdate.Favorite;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return contact;
            }
            return null!;
        }

        public async Task<bool> DeleteContact(int userId, int contactId)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Id == contactId);

            if (contact == null)
            {
                return false;
            }
            _context.Contacts.Remove(contact);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
