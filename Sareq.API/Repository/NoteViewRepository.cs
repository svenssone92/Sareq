using Microsoft.EntityFrameworkCore;
using Sareq.API.Data;
using Sareq.API.Models;
using Sareq.API.Repository.Contracts;

namespace Sareq.API.Repository
{
    public class NoteViewRepository : INoteViewRepository
    {
        private readonly DataContext _context;
        public NoteViewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NoteView view)
        {
            await _context.NoteViews.AddAsync(view);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePastYearAsync()
        {
            var oldViews = _context.NoteViews.Where(v => v.ViewedAt < DateTime.UtcNow.AddYears(-1)).ToList();
            _context.NoteViews.RemoveRange(oldViews);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> GetMostViewedAsync(DateTime since, int count)
        {
            var grouped = await _context.NoteViews
                .Where(v => v.ViewedAt > since)
                .GroupBy(v => v.NoteId)
                .Select(g => new { NoteId = g.Key, ViewCount = g.Count() })
                .OrderByDescending(g => g.ViewCount)
                .Take(count)
                .ToListAsync();

            var noteIds = grouped.Select(g => g.NoteId).ToList();

            var notes = await _context.Notes
                .Where(n => noteIds.Contains(n.Id))
                .ToListAsync();


            return notes.OrderBy(n => noteIds.IndexOf(n.Id));


        }
    }
}
