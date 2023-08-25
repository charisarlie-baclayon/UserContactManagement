using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactInformation.WebAPI.Context
{
    /// <summary>
    /// Represents the database context for Contact Information.
    /// </summary>
    public class ContactInformationDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the collection of users in the database.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Gets or sets the collection of contacts in the database.
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the collection of addresses in the database.
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of audit logs in the database.
        /// </summary>
        public DbSet<AuditTrail> AuditLogs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInformationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public ContactInformationDbContext(DbContextOptions<ContactInformationDbContext> options) : base(options)
        {
        }
        public void Initialize()
        {
            Database.Migrate();
            SaveChanges();
        }
    }
}
