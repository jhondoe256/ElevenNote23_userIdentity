using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.NoteModels
{
    public class NoteEditVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title cannot exeed 100 characters.")]
        [MinLength(2, ErrorMessage = "Title cannot be less than 2 characters.")]
        public string Title { get; set; } = String.Empty;

        [Required]
        [MaxLength(200, ErrorMessage = "Content cannot exeed 200 characters.")]
        public string Content { get; set; } = String.Empty;

        [Required]
        public int CategoryEntityId { get; set; }
    }
}