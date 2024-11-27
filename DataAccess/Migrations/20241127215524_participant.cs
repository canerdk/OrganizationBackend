using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class participant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Organizations_OrganizationId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_OrganizationId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Participants");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Participants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_OrganizationId",
                table: "Participants",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Organizations_OrganizationId",
                table: "Participants",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
