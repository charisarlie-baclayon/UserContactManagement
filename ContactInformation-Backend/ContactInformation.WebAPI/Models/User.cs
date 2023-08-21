using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Models
{
    /// <summary>
    /// Represents a user entity in the system.
    /// </summary>
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password hash for the user.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the password salt for the user.
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the list of contacts associated with the user.
        /// </summary>
        public List<Contact> Contacts { get; set; } 
            = new List<Contact>();

    }
}
