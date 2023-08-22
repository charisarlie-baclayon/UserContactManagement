using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactInformation.WebAPI.Models
{
    /// <summary>
    /// Represents an audit entity in the system.
    /// </summary>
    [Table("Audit Logs")]
    public class AuditTrail
    {
        /// <summary>
        /// Gets or sets the unique identifier of the address.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date time of the audit log.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the user id of the audit log.
        /// </summary>
        public string UserId { get; set; } = string.Empty; // User performing the action

        /// <summary>
        /// Gets or sets the action of the audit log.
        /// </summary>
        public string Action { get; set; } = string.Empty; // Example: "Login", "Register", "Create", "Update", "Delete"
        
        /// <summary>
        /// Gets or sets the entity type of the audit log.
        /// </summary>
        public string EntityType { get; set; } = string.Empty; // Example: "Contact", "Address"
        
        /// <summary>
        /// Gets or sets the entity id of the audit log.
        /// </summary>
        public int EntityId { get; set; } // Id of the affected entity
    }
}
