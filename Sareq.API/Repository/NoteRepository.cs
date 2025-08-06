using Microsoft.EntityFrameworkCore;
using Sareq.API.Data;
using Sareq.API.Models;
using Sareq.API.Repository.Contracts;

namespace Sareq.API.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly DataContext _context;
        public NoteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Note> CreateAsync(Note note)
        {
            var result = await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(Note note)
        {
            if (note is null) throw new ArgumentNullException(nameof(note));

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            return await _context.Notes
                .AsNoTracking()
                .OrderByDescending(n => n.DateMade)
                .ToListAsync();
        }

        public async Task<Note?> GetByIdAsync(int id)
        {
            return await _context.Notes
                .Include(n => n.Elements)
                .SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task UpdateAsync(Note note)
        {
            if (note is null) throw new ArgumentNullException(nameof(note));

            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}
