using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Data.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100,ErrorMessage ="Name cannot exceed 100 characters.")]
        public string Name { get; set; }= null!;
       
        public List<NoteEntity> Notes { get; set; } = new List<NoteEntity>();
    }
}