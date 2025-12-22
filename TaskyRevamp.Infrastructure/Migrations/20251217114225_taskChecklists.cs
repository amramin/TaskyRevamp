using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class taskChecklists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskChecklist_TaskItem_TaskItemId1",
                table: "TaskChecklist");

            migrationBuilder.DropIndex(
                name: "IX_TaskChecklist_TaskItemId1",
                table: "TaskChecklist");

            migrationBuilder.DropColumn(
                name: "TaskItemId1",
                table: "TaskChecklist");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId1",
                table: "TaskChecklist",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
