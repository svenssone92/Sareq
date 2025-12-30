using Microsoft.EntityFrameworkCore;
using Sareq.API.Models;
using Sareq.API.Models.NoteElements;

namespace Sareq.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
                
        }
        public DbSet<Note> Notes { get; set; }

        public DbSet<NoteView> NoteViews { get; set; }

        public DbSet<NoteElement> NoteElements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Elements)
                .WithOne(e => e.Note)
                .HasForeignKey(e => e.NoteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NoteElement>()
                .HasDiscriminator<string>("ElementType")
                .HasValue<TextElement>("text");
        }

    }


}
    