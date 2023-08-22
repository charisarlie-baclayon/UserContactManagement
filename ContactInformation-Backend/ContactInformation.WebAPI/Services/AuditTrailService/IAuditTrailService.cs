using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI.Services.AuditTrailService
{
    /// <summary>
    /// Interface for the AuditTrail Service.
    /// </summary>
    public interface IAuditTrailService
    {
        /// <summary>
        /// Logs audit trail.
        /// </summary>
        /// <param name="action">Action done by user.</param>
        /// <param name="entityType">Entity affected by the action.</param>
        /// <param name="entityId">Id of the entity.</param>
        /// <returns>Void</returns>
        Task LogAuditTrail(string action, string entityType, int entityId);
    }
}
