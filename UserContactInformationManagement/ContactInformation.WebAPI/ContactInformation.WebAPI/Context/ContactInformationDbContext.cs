using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactInformation.WebAPI.Context
{
    public class ContactInformationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Contact> UserContacts { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;

        public ContactInformationDbContext(DbContextOptions<ContactInformationDbContext> options) :base(options)
        {
        }

    }
}
