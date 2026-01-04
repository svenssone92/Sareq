using Sareq.API.Models;
using Sareq.Shared.DTOs;

namespace Sareq.API.Mapping
{
    public static class NoteMapper
    {

        public static NoteDto ToDto(Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                IsPinned = note.IsPinned,
                DateMade = note.DateMade,
                Blocks = note.Blocks.Select(NoteBlockMapper.ToDto).ToList()
            };
        }

        public static ListedNoteDto ToListedDto(Note note)
        {
            return new ListedNoteDto
            {
                Id = note.Id,
                Title = note.Title,
                IsPinned = note.IsPinned,
                DateMade = note.DateMade,
            };
        }

        public static Note ToDomain(CreateNoteDto noteDto)
        {
            return new Note
            {
                Title = noteDto.Title ?? "",
                IsPinned = noteDto.IsPinned,
                Blocks = noteDto.Blocks.Select(NoteBlockMapper.ToDomain).ToList()
            };
        }

        public static Note UpdateToDomain(Note existingNote, UpdateNoteDto updatedNote)
        {
            existingNote.Title = updatedNote.Title ?? "";
            existingNote.IsPinned = updatedNote.IsPinned;

            // Simplified: replace elements for now
            existingNote.Blocks = updatedNote.Blocks.Select(NoteBlockMapper.ToDomain).ToList();

            return existingNote;
        }
    }
}
