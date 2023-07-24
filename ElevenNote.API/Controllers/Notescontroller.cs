using ElevenNote.Models.NoteModels;
using ElevenNote.Services.NoteServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class Notescontroller : ControllerBase
    {
        private readonly INoteService _noteSerice;

        public Notescontroller(INoteService noteSerice)
        {
            _noteSerice = noteSerice;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _noteSerice.GetNotes();

            return Ok(notes);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNoteById(int id)
        {
            var note = await _noteSerice.GetNote(id);
            if (note is null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNote([FromBody] NoteCreateVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _noteSerice.CreateNote(model))
                return Ok("Note Created Successfully.");
            else
                return StatusCode(500, "Internal Server Error.");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNote([FromBody] NoteEditVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _noteSerice.UpdateNote(model))
                return NoContent();
            else
                return StatusCode(500, "Internal Server Error.");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _noteSerice.DeleteNote(id))
                return NoContent();
            else
                return StatusCode(500, "Internal Server Error.");
        }
    }
}