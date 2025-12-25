using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sareq.API.Migrations
{
    /// <inheritdoc />
    public partial class AddingXandYpropstoNoteElement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "X",
                table: "NoteElements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Y",
                table: "NoteElements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X",
                table: "NoteElements");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "NoteElements");
        }
    }
}
