using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _1612240028 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "UserContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "UserContracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserContracts_EventId",
                table: "UserContracts",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserContracts_Events_EventId",
                table: "UserContracts",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserContracts_Events_EventId",
                table: "UserContracts");

            migrationBuilder.DropIndex(
                name: "IX_UserContracts_EventId",
                table: "UserContracts");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "UserContracts");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "UserContracts");
        }
    }
}
