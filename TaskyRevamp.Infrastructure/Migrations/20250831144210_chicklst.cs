using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chicklst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ChecklistItem",
                newName: "TitleEnglish");

            migrationBuilder.AddColumn<string>(
                name: "TitleArabic",
                table: "TaskChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleEnglish",
                table: "TaskChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskChecklistId",
                table: "ChecklistItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedUserId",
                table: "ChecklistItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ChecklistItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ChecklistItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TitleArabic",
                table: "ChecklistItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_AssignedUserId",
                table: "ChecklistItem",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem",
                column: "TaskChecklistId",
                principalTable: "TaskChecklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Users_AssignedUserId",
                table: "ChecklistItem",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Users_AssignedUserId",
                table: "ChecklistItem");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItem_AssignedUserId",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "TitleArabic",
                table: "TaskChecklist");

            migrationBuilder.DropColumn(
                name: "TitleEnglish",
                table: "TaskChecklist");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "TitleArabic",
                table: "ChecklistItem");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                table: "ChecklistItem",
                newName: "Description");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskChecklistId",
                table: "ChecklistItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem",
                column: "TaskChecklistId",
                principalTable: "TaskChecklist",
                principalColumn: "Id");
        }
    }
}
