using System.ComponentModel.DataAnnotations;

namespace BookStoreMvcAppW.Models.DTO
{
    public class RegistrationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }

        // RegEx for secure password with User interactive Error message and help
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one number, and must be between 8 and 15 characters long.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        public string Role { get; set; }
    }
}
