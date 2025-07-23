using Microsoft.EntityFrameworkCore;
using Sareq.API.Data;
using Sareq.API.Models;
using Sareq.API.Repositorys;

namespace Sareq.Tests.Repository
{
    public class NoteRepositoryTests
    {
        [Fact]
        public async Task CreateAsync_ShouldAddNote()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var note = new Note { Title = "Test Note" };

            // Act
            await using (var actContext = new DataContext(options))
            {
                var repo = new NoteRepository(actContext);
                var created = await repo.CreateAsync(note);
            }
            

            // Assert
            await using (var assertContext = new DataContext(options))
            {
                var savedNote = await assertContext.Notes.SingleAsync();
                Assert.Equal("Test Note", savedNote.Title);
            }
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteNote()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using (var seedContext = new DataContext(options))
            {
                seedContext.Notes.Add(new Note { Title = "Should Be Deleted" });
                seedContext.Notes.Add(new Note { Title = "Should Be Kept" });
                await seedContext.SaveChangesAsync();
            }

            Note noteToBeDeleted;
            await using (var fetchContext = new DataContext(options))
            {
                noteToBeDeleted = await fetchContext.Notes.FirstAsync(n => n.Title == "Should Be Deleted");
            }


            // Act
            await using (var actContext = new DataContext(options))
            {
                var repo = new NoteRepository(actContext);
                await repo.DeleteAsync(noteToBeDeleted);
            }

            // Assert
            await using (var assertContext = new DataContext(options))
            {
                var titles = await assertContext.Notes.Select(n => n.Title).ToListAsync();
                Assert.Contains("Should Be Kept", titles);
                Assert.DoesNotContain("Should Be Deleted", titles);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllNotes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using (var seedContext = new DataContext(options))
            {
                seedContext.Notes.Add(new Note { Title = "Note 1" });
                seedContext.Notes.Add(new Note { Title = "Note 2" });
                await seedContext.SaveChangesAsync();
            }


            // Act
            List<Note> notes;
            await using (var actContext = new DataContext(options))
            {
                var repo = new NoteRepository(actContext);
                notes = (await repo.GetAllAsync()).ToList();
            }

            // Assert
            Assert.Equal(2, notes.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNoteById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using (var seedContext = new DataContext(options))
            {
                seedContext.Notes.Add(new Note { Title = "Note 1" });
                seedContext.Notes.Add(new Note { Title = "Note 2" });
                await seedContext.SaveChangesAsync();
            }

            Note noteToGet;
            await using (var fetchContext = new DataContext(options))
            {
                noteToGet = await fetchContext.Notes.SingleAsync(n => n.Title == "Note 2");
            }

            // Act
            Note? note;
            await using (var actContext = new DataContext(options))
            {
                var repo = new NoteRepository(actContext);
                note = await repo.GetByIdAsync(noteToGet.Id);
            }

            // Assert
            Assert.NotNull(note);
            Assert.Equal("Note 2", note.Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateNote()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using (var seedContext = new DataContext(options))
            {
                seedContext.Notes.Add(new Note { Title = "New Note" });
                await seedContext.SaveChangesAsync();
            }

            Note detachedNote;
            await using (var fetchContext = new DataContext(options))
            {
                detachedNote = await fetchContext.Notes.SingleAsync();
                detachedNote.Title = "Updated Note";
            }


            // Act
            await using (var actContext = new DataContext(options))
            {
                var repo = new NoteRepository(actContext);
                await repo.UpdateAsync(detachedNote);
            }

            // Assert
            await using (var assertContext = new DataContext(options))
            {
                var updatedNote = await assertContext.Notes.SingleAsync();
                Assert.Equal("Updated Note", updatedNote.Title);
            }
        }

    }
}
