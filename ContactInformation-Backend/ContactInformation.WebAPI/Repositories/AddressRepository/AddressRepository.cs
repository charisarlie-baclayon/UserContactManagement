using ContactInformation.WebAPI.Context;
using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactInformation.WebAPI.Repositories.AddressRepository
{
    /// <summary>
    /// Implementation for performing address-related data operations.
    /// </summary>
    public class AddressRepository : IAddressRepository
    {
        private readonly ContactInformationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the AddressRepository class.
        /// </summary>
        /// <param name="context">The database context for accessing address-related data.</param>
        public AddressRepository(ContactInformationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<int> CreateAddress(int contactId, Address addressToAdd)
        {
            _context.Addresses.Add(addressToAdd);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return addressToAdd.Id;
            }
            return 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAddress(int contactId, int addressId)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.ContactId == contactId && a.Id == addressId);

            if (address == null)
            {
                return false;
            }

            _context.Addresses.Remove(address);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public async Task<Address> GetAddress(int contactId, int addressId)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.ContactId == contactId && a.Id == addressId);
            return address!;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Address>> GetAddresses(int contactId)
        {
            var addresses = await _context.Addresses
                .Where(a => a.ContactId == contactId).ToListAsync();
            return addresses;
        }

        /// <inheritdoc />
        public async Task<Address> UpdateAddress(int contactId, Address addressToUpdate)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.ContactId == contactId && a.Id == addressToUpdate.Id);
            if (address == null)
            {
                return null!;
            }

            address.AddressDescription = addressToUpdate.AddressDescription;
            address.AddressType = addressToUpdate.AddressType;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return address;
            }
            return null!;
        }
    }
}
