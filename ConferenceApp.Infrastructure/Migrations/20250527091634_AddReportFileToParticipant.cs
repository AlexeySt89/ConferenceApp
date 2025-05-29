using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReportFileToParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Participants",
                newName: "ReportFileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "ReportFile",
                table: "Participants",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportFile",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "ReportFileName",
                table: "Participants",
                newName: "FilePath");
        }
    }
}
