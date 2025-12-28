using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chanetaskitemcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileMapping");

            migrationBuilder.DropColumn(
                name: "DescriptionArabic",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "TitleArabic",
                table: "TaskItem");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                table: "TaskItem",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DescriptionEnglish",
                table: "TaskItem",
                newName: "Description");

            //migrationBuilder.AddColumn<int>(
            //    name: "Progress",
            //    table: "TaskItem",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "TaskItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId1",
                table: "TaskChecklist",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DelegationToUser",
                table: "GeneralModulePermission",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskChecklist_TaskItemId1",
                table: "TaskChecklist",
                column: "TaskItemId1",
                unique: true,
                filter: "[TaskItemId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskChecklist_TaskItem_TaskItemId1",
                table: "TaskChecklist",
                column: "TaskItemId1",
                principalTable: "TaskItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskChecklist_TaskItem_TaskItemId1",
                table: "TaskChecklist");

            migrationBuilder.DropIndex(
                name: "IX_TaskChecklist_TaskItemId1",
                table: "TaskChecklist");

            //migrationBuilder.DropColumn(
            //    name: "Progress",
            //    table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "TaskItemId1",
                table: "TaskChecklist");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TaskItem",
                newName: "TitleEnglish");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TaskItem",
                newName: "DescriptionEnglish");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionArabic",
                table: "TaskItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleArabic",
                table: "TaskItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "DelegationToUser",
                table: "GeneralModulePermission",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FileMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileMapping", x => x.Id);
                });
        }
    }
}
