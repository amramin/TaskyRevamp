using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedbytaskcomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_Users_UpdateddById",
                table: "TaskComment");

            migrationBuilder.DropIndex(
                name: "IX_TaskComment_UpdateddById",
                table: "TaskComment");

            migrationBuilder.DropColumn(
                name: "UpdateddById",
                table: "TaskComment");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_UpdatedById",
                table: "TaskComment",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComment_Users_UpdatedById",
                table: "TaskComment",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_Users_UpdatedById",
                table: "TaskComment");

            migrationBuilder.DropIndex(
                name: "IX_TaskComment_UpdatedById",
                table: "TaskComment");

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateddById",
                table: "TaskComment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_UpdateddById",
                table: "TaskComment",
                column: "UpdateddById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComment_Users_UpdateddById",
                table: "TaskComment",
                column: "UpdateddById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
