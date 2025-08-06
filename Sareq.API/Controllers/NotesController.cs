using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sareq.API.Models;
using Sareq.API.Models.NoteElements;
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
            var noteDto = MapToNoteDto(note);
            return Ok(noteDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _noteRepository.GetAllAsync();
            var listedDtos = notes.Select(n => new ListedNoteDto
            {
                Id = n.Id,
                Title = n.Title,
                IsPinned = n.IsPinned,
                DateMade = n.DateMade
            }).ToList();

            return Ok(listedDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto createDto)
        {
            var note = new Note
            {
                Title = createDto.Title ?? "",
                IsPinned = createDto.IsPinned,
                Elements = createDto.Elements.Select(MapToElementModel).ToList()
            };

            var createdNote = await _noteRepository.CreateAsync(note);
            var noteDto = MapToNoteDto(createdNote);

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, noteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteDto updateDto)
        {
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.Title = updateDto.Title ?? "";
            existingNote.IsPinned = updateDto.IsPinned;

            // Simplified: replace elements for now
            existingNote.Elements = updateDto.Elements.Select(MapToElementModel).ToList();

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

        // --- Mapping ---
        private NoteDto MapToNoteDto(Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                IsPinned = note.IsPinned,
                DateMade = note.DateMade,
                Elements = note.Elements.Select(MapToElementDto).ToList()
            };
        }

        private NoteElementDto MapToElementDto(NoteElement element)
        {
            return element switch
            {
                TextElement text => new TextElementDto { Id = text.Id, Text = text.Text },
                _ => throw new NotImplementedException("Unknown element type")
            };
        }

        private NoteElement MapToElementModel(NoteElementDto dto)
        {
            return dto switch
            {
                TextElementDto text => new TextElement
                {
                    Id = text.Id,
                    Text = text.Text
                },
                _ => throw new NotImplementedException("Unknown DTO element type")
            };
        }
    }
}
