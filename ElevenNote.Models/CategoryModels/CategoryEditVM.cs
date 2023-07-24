using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.CategoryModels
{
    public class CategoryEditVM
    {
        [Required]
         public int Id { get; set; }
       
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;
    }
}