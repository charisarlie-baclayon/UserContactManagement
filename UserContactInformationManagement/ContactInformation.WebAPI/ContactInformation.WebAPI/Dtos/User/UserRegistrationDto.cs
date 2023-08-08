using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos.User
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [Compare("Password", ErrorMessage = "Password must match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
