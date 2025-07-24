using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sareq.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNoteView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEdited",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "LastViewed",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateEdited",
                table: "Notes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastViewed",
                table: "Notes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
