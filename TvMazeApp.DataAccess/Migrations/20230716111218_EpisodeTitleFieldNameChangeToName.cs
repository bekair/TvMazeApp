using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvMazeApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EpisodeTitleFieldNameChangeToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Episodes");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Episodes",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Episodes",
                newName: "Title");

            migrationBuilder.AddColumn<long>(
                name: "Updated",
                table: "Episodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
