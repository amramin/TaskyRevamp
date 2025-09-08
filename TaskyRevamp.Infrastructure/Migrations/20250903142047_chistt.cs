using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chistt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskChecklist_TaskItem_Id",
                table: "TaskChecklist");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskChecklist_ChecklistId",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_ChecklistId",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "TaskItem");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "TaskChecklist",
                newName: "TaskItemId");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId1",
                table: "TaskChecklist",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskChecklist_TaskItemId",
                table: "TaskChecklist",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskChecklist_TaskItemId1",
                table: "TaskChecklist",
                column: "TaskItemId1",
                unique: true,
                filter: "[TaskItemId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem",
                column: "TaskChecklistId",
                principalTable: "TaskChecklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskChecklist_TaskItem_TaskItemId",
                table: "TaskChecklist",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskChecklist_TaskItem_TaskItemId",
                table: "TaskChecklist");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskChecklist_TaskItem_TaskItemId1",
                table: "TaskChecklist");

            migrationBuilder.DropIndex(
                name: "IX_TaskChecklist_TaskItemId",
                table: "TaskChecklist");

            migrationBuilder.DropIndex(
                name: "IX_TaskChecklist_TaskItemId1",
                table: "TaskChecklist");

            migrationBuilder.DropColumn(
                name: "TaskItemId1",
                table: "TaskChecklist");

            migrationBuilder.RenameColumn(
                name: "TaskItemId",
                table: "TaskChecklist",
                newName: "TaskId");

            migrationBuilder.AddColumn<Guid>(
                name: "ChecklistId",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_ChecklistId",
                table: "TaskItem",
                column: "ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem",
                column: "TaskChecklistId",
                principalTable: "TaskChecklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskChecklist_TaskItem_Id",
                table: "TaskChecklist",
                column: "Id",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskChecklist_ChecklistId",
                table: "TaskItem",
                column: "ChecklistId",
                principalTable: "TaskChecklist",
                principalColumn: "Id");
        }
    }
}
