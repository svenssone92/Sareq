using Sareq.API.Data;
using Sareq.API.Models;
using Sareq.API.Repository.Contracts;

namespace Sareq.API.Repositorys
{
    public class NoteRepository : INoteRepository
    {
        private readonly DataContext _context;
        public NoteRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Note> CreateAsync(Note note)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Note note)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Note>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Note?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
