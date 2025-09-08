using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chicklsttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_TaskItem_TaskItemId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_TaskItemId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "Department");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "TaskChecklist",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_TaskChecklist_TaskItem_Id",
                table: "TaskChecklist",
                column: "Id",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskChecklist_TaskItem_Id",
                table: "TaskChecklist");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "TaskChecklist");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId",
                table: "Department",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_TaskItemId",
                table: "Department",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_TaskItem_TaskItemId",
                table: "Department",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");
        }
    }
}
