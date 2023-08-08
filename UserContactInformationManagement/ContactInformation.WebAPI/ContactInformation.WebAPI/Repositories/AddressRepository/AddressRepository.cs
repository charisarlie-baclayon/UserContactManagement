using ContactInformation.WebAPI.Context;
using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactInformation.WebAPI.Repositories.AddressRepository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ContactInformationDbContext _context;

        public AddressRepository(ContactInformationDbContext context)
        {
            _context = context;
        }
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

        public async Task<Address> GetAddress(int contactId, int addressId)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.ContactId == contactId && a.Id == addressId);
            return address!;
        }

        public async Task<IEnumerable<Address>> GetAddresses(int contactId)
        {
            var addresses = await _context.Addresses
                .Where(a => a.ContactId == contactId).ToListAsync();
            return addresses;
        }

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
