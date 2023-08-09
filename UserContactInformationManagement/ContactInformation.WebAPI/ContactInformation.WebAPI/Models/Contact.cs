using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Models
{
    /// <summary>
    /// Represents a contact entity associated with a user in the system.
    /// </summary>
    [Table("Contacts")]
    public class Contact
    {
        /// <summary>
        /// Gets or sets the unique identifier for the contact.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the contact.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the contact.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of addresses associated with the contact.
        /// </summary>
        public List<Address> Addresses { get; set; }
            = new List<Address>();

        /// <summary>
        /// Gets or sets the phone number of the contact.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the contact.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the birth date of the contact.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the contact is a favorite.
        /// </summary>
        public bool Favorite { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the contact.
        /// </summary>
        [ForeignKey("UserId")]
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user associated with the contact.
        /// </summary>
        public int UserId { get; set; }
    }
}
