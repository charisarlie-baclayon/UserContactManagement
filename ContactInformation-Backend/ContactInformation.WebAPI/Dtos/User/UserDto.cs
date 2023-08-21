using System.ComponentModel.DataAnnotations;

namespace ContactInformation.WebAPI.Dtos.User
{
    /// <summary>
    /// Data transfer object for a user's basic information.
    /// </summary>
    public class UserDto
    {
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
    }
}
