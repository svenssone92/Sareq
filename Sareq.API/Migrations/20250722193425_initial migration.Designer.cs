﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sareq.API.Data;

#nullable disable

namespace Sareq.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250722193425_initial migration")]
    partial class initialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sareq.API.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateEdited")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateMade")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPinned")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastViewed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Sareq.API.Models.NoteElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ElementType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int>("NoteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("NoteElements");

                    b.HasDiscriminator<string>("ElementType").HasValue("NoteElement");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Sareq.API.Models.NoteElements.TextElement", b =>
                {
                    b.HasBaseType("Sareq.API.Models.NoteElement");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Text");
                });

            modelBuilder.Entity("Sareq.API.Models.NoteElement", b =>
                {
                    b.HasOne("Sareq.API.Models.Note", "Note")
                        .WithMany("Elements")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Sareq.API.Models.Note", b =>
                {
                    b.Navigation("Elements");
                });
#pragma warning restore 612, 618
        }
    }
}
