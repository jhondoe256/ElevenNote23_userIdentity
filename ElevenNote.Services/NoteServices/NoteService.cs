using System.Security.Claims;
using AutoMapper;
using ElevenNote.Data.ElevenNoteContext;
using ElevenNote.Data.Entities;
using ElevenNote.Models.NoteModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ElevenNote.Services.NoteServices
{
    public class NoteService : INoteService
    {
        private readonly ElevenNoteDBContext _context;
        private IMapper _mapper;

        private readonly string _userId;

        public NoteService(IHttpContextAccessor httpContextAccessor, ElevenNoteDBContext context, IMapper mapper)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims!.FindFirst("uId")!.Value;

            _userId = value;

            if (_userId is null)
                throw new Exception("Attemped to build NoteService with User Id claim.");

            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateNote(NoteCreateVM model)
        {
            var note = _mapper.Map<NoteEntity>(model);

            note.OwnerId = _userId;
            note.CreatedUTC = DateTimeOffset.Now;

            await _context.Notes.AddAsync(note);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note?.OwnerId != _userId)
                return false;

            _context.Notes.Remove(note);

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<NoteDetailVM> GetNote(int id)
        {
            var note = await _context.Notes.Include(n=>n.Owner).Include(n=>n.Category).FirstOrDefaultAsync(x => x.Id == id && x.OwnerId == _userId);

            if (note is null) return new NoteDetailVM();

            var noteDetail = _mapper.Map<NoteDetailVM>(note);
            
            noteDetail.CatetoryName = note.Category.Name;
            noteDetail.OwnerName = note.Owner.FullName;

            return noteDetail;
        }

        public async Task<List<NoteListItemVM>> GetNotes()
        {
            var notes = await _context.Notes.Where(n => n.OwnerId == _userId).ToListAsync();

            var noteListItems = _mapper.Map<List<NoteListItemVM>>(notes);

            return noteListItems;
        }

        public async Task<bool> UpdateNote(NoteEditVM model)
        {
            var note = await _context.Notes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (note?.OwnerId != _userId)
                return false;

            var conversion = _mapper.Map<NoteEditVM, NoteEntity>(model, note);
            conversion.ModifiedUTC = DateTimeOffset.UtcNow;
            
            _context.Notes.Update(conversion);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}