using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class xxxx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TaskItem");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_StatusId",
                table: "TaskItem",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_StatusSettings_StatusId",
                table: "TaskItem",
                column: "StatusId",
                principalTable: "StatusSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_StatusSettings_StatusId",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_StatusId",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TaskItem");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TaskItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
