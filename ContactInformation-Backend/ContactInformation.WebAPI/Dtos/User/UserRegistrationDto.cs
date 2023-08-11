using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos.User
{
    /// <summary>
    /// Data transfer object for user registration.
    /// </summary>
    public class UserRegistrationDto
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation password for password validation.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [PasswordPropertyText]
        [Compare("Password", ErrorMessage = "Password must match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
