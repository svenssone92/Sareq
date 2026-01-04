using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sareq.API.Mapping;
using Sareq.API.Models;
using Sareq.API.Models.NoteBlocks;
using Sareq.API.Repository.Contracts;
using Sareq.Shared.DTOs;

namespace Sareq.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(NoteMapper.ToDto(note));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _noteRepository.GetAllAsync();

            return Ok(notes.Select(n => NoteMapper.ToListedDto(n)).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto createNoteDto)
        {
            var noteToCreate = NoteMapper.ToDomain(createNoteDto);

            var createdNote = await _noteRepository.CreateAsync(noteToCreate);
            var noteDto = NoteMapper.ToDto(createdNote);

            return CreatedAtAction(nameof(GetNote), new { id = createdNote.Id }, noteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteDto updateDto)
        {
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote == null)
            {
                return NotFound();
            }

            NoteMapper.UpdateToDomain(existingNote, updateDto);

            await _noteRepository.UpdateAsync(existingNote);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            await _noteRepository.DeleteAsync(note);
            return NoContent();
        }
    }
}
