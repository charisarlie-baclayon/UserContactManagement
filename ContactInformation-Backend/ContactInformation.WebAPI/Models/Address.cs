using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Models
{
    /// <summary>
    /// Represents an address entity associated with a contact in the system.
    /// </summary>
    [Table("Addresses")]
    public class Address
    {
        /// <summary>
        /// Gets or sets the unique identifier of the address.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the address.
        /// </summary>
        public string AddressDescription { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the address (e.g., Delivery, Billing, Home, Work).
        /// </summary>
        public string AddressType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact associated with this address.
        /// </summary>
        [ForeignKey("ContactId")]
        public Contact? Contact { get; set; }

        /// <summary>
        /// Gets or sets the ID of the contact associated with this address.
        /// </summary>
        public int ContactId { get; set; }
    }
}
