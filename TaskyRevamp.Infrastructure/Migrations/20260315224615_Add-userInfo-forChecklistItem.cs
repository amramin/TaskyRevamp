using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdduserInfoforChecklistItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ChecklistItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "ChecklistItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_CreatedById",
                table: "ChecklistItem",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Users_CreatedById",
                table: "ChecklistItem",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Users_CreatedById",
                table: "ChecklistItem");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItem_CreatedById",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ChecklistItem");
        }
    }
}
