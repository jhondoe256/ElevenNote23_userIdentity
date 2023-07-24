using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.UserModels
{
    public class UserEntityVM : LoginVM
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
    }
}