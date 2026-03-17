using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddchecklistTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Users_CreatedById",
                table: "ChecklistItem");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItem_CreatedById",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "TitleArabic",
                table: "TaskChecklist");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "TitleArabic",
                table: "ChecklistItem");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                table: "TaskChecklist",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                table: "ChecklistItem",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "ChecklistItem",
                newName: "IsDone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ChecklistItem",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedUserId",
                table: "ChecklistItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TaskChecklist",
                newName: "TitleEnglish");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ChecklistItem",
                newName: "TitleEnglish");

            migrationBuilder.RenameColumn(
                name: "IsDone",
                table: "ChecklistItem",
                newName: "IsCompleted");

            migrationBuilder.AddColumn<string>(
                name: "TitleArabic",
                table: "TaskChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ChecklistItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedUserId",
                table: "ChecklistItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
    }
}
