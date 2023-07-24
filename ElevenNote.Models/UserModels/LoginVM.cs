using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.UserModels
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(15, ErrorMessage = "The password has a limit of {2} to {1} characters", MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }
}