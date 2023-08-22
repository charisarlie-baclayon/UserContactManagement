using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Repositories;
using System.Security.Claims;

namespace ContactInformation.WebAPI.Services.AuditTrailService
{
    /// <summary>
    /// Implementation for performing audit-related data operations.
    /// </summary>
    public class AuditTrailService : IAuditTrailService
    {
        private readonly IAuditTrailRepository _auditTrailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the AuditTrailService class.
        /// </summary>
        /// <param name="auditTrailRepository"></param>
        /// <param name="httpContextAccessor"></param>
        public AuditTrailService(IAuditTrailRepository auditTrailRepository, IHttpContextAccessor httpContextAccessor)
        {
            _auditTrailRepository = auditTrailRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public async Task LogAuditTrail(string action, string entityType, int entityId)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var auditTrail = new AuditTrail
            {
                Timestamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Asia/Singapore")),
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                UserId = userId ?? string.Empty
            };

            await _auditTrailRepository.LogAuditTrail(auditTrail);
        }
    }
}
