using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.CategoryModels
{
    public class CategoryCreateVM
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;
    }
}