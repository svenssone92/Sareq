using Microsoft.EntityFrameworkCore;
using Sareq.API.Data;
using Sareq.API.Models;
using Sareq.API.Repository;
using System;

namespace Sareq.Tests.Repository
{
    public class NoteViewRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ShouldAddNoteView()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            int noteId;

            await using (var seedContext = new DataContext(options))
            {
                var note = new Note { Title = "Test Note" };
                seedContext.Notes.Add(note);
                await seedContext.SaveChangesAsync();
                noteId = note.Id;
            }

            // Act
            await using (var actContext = new DataContext(options))
            {
                var repo = new NoteViewRepository(actContext);
                var noteView = new NoteView
                {
                    NoteId = noteId,
                    ViewedAt = DateTime.UtcNow
                };
                await repo.AddAsync(noteView);
            }


            // Assert
            await using (var assertContext = new DataContext(options))
            {
                var fetchedNoteView = await assertContext.NoteViews
                    .Include(nv => nv.Note)
                    .SingleAsync();
                Assert.Equal(noteId, fetchedNoteView.NoteId);
            }
        }

        [Fact]
        public async Task GetMostViewedAsync_ShouldReturnAGivenNrOfNotesMostViewedOverGivenTime()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            int note1Id;
            int note2Id;
            int note3Id;

            await using (var seedContext = new DataContext(options))
            {
                var note1 = new Note { Title = "Test Note1" };
                var note2 = new Note { Title = "Test Note2" };
                var note3 = new Note { Title = "Test Note3" };

                seedContext.Notes.AddRange(note1, note2, note3);

                await seedContext.SaveChangesAsync();

                note1Id = note1.Id;
                note2Id = note2.Id;
                note3Id = note3.Id;

                seedContext.NoteViews.AddRange
                    (
                        new NoteView { NoteId = note1Id, ViewedAt = DateTime.UtcNow },

                        new NoteView { NoteId = note2Id, ViewedAt = DateTime.UtcNow.AddDays(-8) },
                        new NoteView { NoteId = note2Id, ViewedAt = DateTime.UtcNow.AddDays(-8) },
                        new NoteView { NoteId = note2Id, ViewedAt = DateTime.UtcNow },
                        new NoteView { NoteId = note2Id, ViewedAt = DateTime.UtcNow },

                        new NoteView { NoteId = note3Id, ViewedAt = DateTime.UtcNow },
                        new NoteView { NoteId = note2Id, ViewedAt = DateTime.UtcNow },
                        new NoteView { NoteId = note2Id, ViewedAt = DateTime.UtcNow }

                    );

                await seedContext.SaveChangesAsync();
            }

            // Act
            List<Note> result1;
            List<Note> result2;
            await using (var actContext = new DataContext(options))
            {
                var repo = new NoteViewRepository(actContext);

                DateTime since1 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(7));
                DateTime since2 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(8));

                result1 = (await repo.GetMostViewedAsync(since1, 2)).ToList();
                result2 = (await repo.GetMostViewedAsync(since2, 3)).ToList();
            }

            // Assert
            Assert.Equal(2, result1.Count);
            Assert.Equal(note3Id, result1[0].Id); // most recent views
            Assert.Equal(note2Id, result1[1].Id);

            Assert.Equal(3, result2.Count);
            Assert.Equal(note2Id, result2[0].Id); // most recent views
            Assert.Equal(note3Id, result2[1].Id);
            Assert.Equal(note1Id, result2[2].Id);
        }


        [Fact]
        public async Task DeletePastYearAsync_ShouldDeleteNoteViewsOlderThanAYear()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            int noteId;

            await using (var seedContext = new DataContext(options))
            {
                var note = new Note { Title = "Test Note" };
                seedContext.Notes.Add(note);
                await seedContext.SaveChangesAsync();
                noteId = note.Id;

                seedContext.NoteViews.AddRange
                (
                    new NoteView { NoteId = noteId, ViewedAt = DateTime.UtcNow.AddDays(-366) },
                    new NoteView { NoteId = noteId, ViewedAt = DateTime.UtcNow }
                );
            }

            // Act
            await using (var actContext = new DataContext(options))
            {
                var repo = new NoteViewRepository(actContext);
                await repo.DeletePastYearAsync();
            }


            // Assert
            await using (var assertContext = new DataContext(options))
            {
                List<NoteView> fetchedNoteViews = await assertContext.NoteViews.ToListAsync();
                Assert.Single(fetchedNoteViews);
                Assert.True(fetchedNoteViews[0].ViewedAt > DateTime.UtcNow.AddYears(-1));
            }
        }
    }
}
