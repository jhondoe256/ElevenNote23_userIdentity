using ElevenNote.Models.NoteModels;

namespace ElevenNote.Models.CategoryModels
{
    public class CategoryDetailVM
    {
         public int Id { get; set; }
        
        public string Name { get; set; }= null!;
       
         public List<NoteListItemVM> Notes { get; set; } = new List<NoteListItemVM>();
    }
}