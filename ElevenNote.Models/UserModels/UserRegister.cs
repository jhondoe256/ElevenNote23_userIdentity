using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.UserModels
{
    public class UserRegister
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(4)]
        public string UserName { get; set; } = null!;

        [Required]
        [MinLength(4)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}