using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSectionAndArticleFileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportFileName",
                table: "Participants",
                newName: "Section");

            migrationBuilder.RenameColumn(
                name: "ReportFile",
                table: "Participants",
                newName: "ArticleFile");

            migrationBuilder.AddColumn<byte[]>(
                name: "ApplicationFile",
                table: "Participants",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationFileName",
                table: "Participants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArticleFileName",
                table: "Participants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationFile",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ApplicationFileName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ArticleFileName",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "Section",
                table: "Participants",
                newName: "ReportFileName");

            migrationBuilder.RenameColumn(
                name: "ArticleFile",
                table: "Participants",
                newName: "ReportFile");
        }
    }
}
