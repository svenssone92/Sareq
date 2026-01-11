using Microsoft.EntityFrameworkCore;
using Sareq.API.Models;
using Sareq.API.Models.NoteBlocks;
using Sareq.API.Models.RichText;
using System.Text.Json;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NoteBlock>()
                .HasDiscriminator<string>("BlockType")
                .HasValue<TextBlock>("Text");

            modelBuilder.Entity<TextBlock>()
                .Property(b => b.Spans)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<TextSpan>>(v, (JsonSerializerOptions?)null)!
                );

        }

    }


}
    