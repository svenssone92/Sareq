using Microsoft.EntityFrameworkCore;
using Sareq.API.Models;
using Sareq.API.Models.NoteBlocks;

namespace Sareq.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
                
        }
        public DbSet<Note> Notes { get; set; }

        public DbSet<NoteView> NoteViews { get; set; }

        public DbSet<NoteBlock> NoteBlocks { get; set; }

        //Can you explain this?
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NoteBlock>()
                .HasDiscriminator<string>("BlockType")
                .HasValue<TextBlock>("Text");
        }

    }


}
    