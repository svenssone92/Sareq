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
        public DbSet<TextElement> TextElements { get; set; }

        //Can you explain this?
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NoteElement>()
                .HasDiscriminator<string>("ElementType")
                .HasValue<TextElement>("Text");
        }

    }


}
    