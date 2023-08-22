using ContactInformation.WebAPI.Context;
using ContactInformation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactInformation.WebAPI.Repositories.AuditTrailRepository
{
    /// <summary>
    /// Implementation for performing audit-related data operations.
    /// </summary>
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly ContactInformationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the AuditTrailRepository class.
        /// </summary>
        /// <param name="context">The database context for accessing audit-related data.</param>
        public AuditTrailRepository(ContactInformationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task LogAuditTrail(AuditTrail auditTrail)
        {
            _context.AuditLogs.Add(auditTrail);
            await _context.SaveChangesAsync();
        }
    }
}
