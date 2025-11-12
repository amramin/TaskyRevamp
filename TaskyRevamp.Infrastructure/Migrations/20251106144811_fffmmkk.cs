using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fffmmkk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TaskItem_TaskItemId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TaskItemId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskItemId",
                table: "Users",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TaskItem_TaskItemId",
                table: "Users",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
