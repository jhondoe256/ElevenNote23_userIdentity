using ElevenNote.Models.NoteModels;

namespace ElevenNote.Services.NoteServices
{
    public interface INoteService
    {
        Task<bool> CreateNote(NoteCreateVM model);
        Task<bool> UpdateNote(NoteEditVM model);
        Task<bool> DeleteNote(int id);
        Task<NoteDetailVM> GetNote(int id);
        Task<List<NoteListItemVM>> GetNotes();
    }
}