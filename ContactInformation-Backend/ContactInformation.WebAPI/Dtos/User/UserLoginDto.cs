using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos.User
{
    /// <summary>
    /// Data transfer object for user login.
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
