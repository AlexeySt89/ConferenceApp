﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Participants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Participants");
        }
    }
}
