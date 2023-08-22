using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Repositories
{
    /// <summary>
    /// Interface for the AuditTrail repository.
    /// </summary>
    public interface IAuditTrailRepository
    {
        /// <summary>
        /// Logs audit trail.
        /// </summary>
        /// <param name="auditTrail">Audit log to be added.</param>
        /// <returns>Void</returns>
        Task LogAuditTrail(AuditTrail auditTrail);
    }
}
