using ContactInformation.WebAPI.Context;
using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactInformation.WebAPI.Repositories.ContactRepository
{
    /// <summary>
    /// Implementation for performing contact-related data operations.
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        private readonly ContactInformationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the ContactRepository class.
        /// </summary>
        /// <param name="context">The database context for accessing contact-related data.</param>
        public ContactRepository(ContactInformationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Contact>> GetContacts(int userId)
        {
            var contacts = await _context.Contacts
                .Where(c => c.UserId == userId)
                .Include(c => c.Addresses) // Include Addresses
                .ToListAsync();
            return contacts;
        }

        /// <inheritdoc />
        public async Task<Contact> GetContact(int userId, int contactId)
        {
            var contact = await _context.Contacts
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Id == contactId);
            return contact!;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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
