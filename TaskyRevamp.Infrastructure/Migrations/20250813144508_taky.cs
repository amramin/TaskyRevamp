using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class taky : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskAssignees_AssigneesId",
                table: "TaskItem");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TaskItem",
                newName: "TitleEnglish");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TaskItem",
                newName: "TitleArabic");

            migrationBuilder.RenameColumn(
                name: "AssigneesId",
                table: "TaskItem",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItem_AssigneesId",
                table: "TaskItem",
                newName: "IX_TaskItem_CreatedById");

            migrationBuilder.AddColumn<string>(
                name: "AssignedDepartmentIds",
                table: "TaskItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "AssignedIds",
                table: "TaskItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "TaskItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DescriptionArabic",
                table: "TaskItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEnglish",
                table: "TaskItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "TaskItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateddById",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId",
                table: "TaskAssignees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_UpdateddById",
                table: "TaskItem",
                column: "UpdateddById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignees_TaskItemId",
                table: "TaskAssignees",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignees_TaskItem_TaskItemId",
                table: "TaskAssignees",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Users_CreatedById",
                table: "TaskItem",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Users_UpdateddById",
                table: "TaskItem",
                column: "UpdateddById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignees_TaskItem_TaskItemId",
                table: "TaskAssignees");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_CreatedById",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_UpdateddById",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_UpdateddById",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskAssignees_TaskItemId",
                table: "TaskAssignees");

            migrationBuilder.DropColumn(
                name: "AssignedDepartmentIds",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "AssignedIds",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "DescriptionArabic",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "DescriptionEnglish",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "UpdateddById",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "TaskAssignees");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                table: "TaskItem",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleArabic",
                table: "TaskItem",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TaskItem",
                newName: "AssigneesId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItem_CreatedById",
                table: "TaskItem",
                newName: "IX_TaskItem_AssigneesId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskAssignees_AssigneesId",
                table: "TaskItem",
                column: "AssigneesId",
                principalTable: "TaskAssignees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
