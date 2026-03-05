using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdddeleteTaskProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TaskItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_DeletedById",
                table: "TaskItem",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Users_DeletedById",
                table: "TaskItem",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_DeletedById",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_DeletedById",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "TaskItem");
        }
    }
}
