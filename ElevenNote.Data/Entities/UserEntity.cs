using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ElevenNote.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string LastName { get; set; } = string.Empty;

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public DateTime DateCreated { get; set; }

        public List<NoteEntity> Notes { get; set; } = new List<NoteEntity>();
    }
}