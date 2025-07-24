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

        public Task AddAsync(NoteView view)
        {
            throw new NotImplementedException();
        }

        public Task DeletePastYearAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Note>> GetMostViewedAsync(DateTime since, int count)
        {
            throw new NotImplementedException();
        }
    }
}
