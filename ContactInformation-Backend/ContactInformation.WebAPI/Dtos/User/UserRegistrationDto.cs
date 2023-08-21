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
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        /// [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")][Required(ErrorMessage = "Last Name is required.")]
        
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email must not exceed 100 characters.")]
        public string Email { get; set; } = string.Empty;

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
