using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class assgn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_TaskAssignees_TaskAssigneesId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_TaskAssignees_TaskAssigneesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TaskAssigneesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Department_TaskAssigneesId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "TaskAssigneesId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskAssigneesId",
                table: "Department");

            migrationBuilder.AddColumn<bool>(
                name: "AllowComplete",
                table: "TaskAssignees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "TaskAssignees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TaskAssignees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "TaskAssignees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                table: "TaskAssignees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TaskAssignees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "taskId",
                table: "TaskAssignees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignees_CreatedById",
                table: "TaskAssignees",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignees_taskId",
                table: "TaskAssignees",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignees_UserId",
                table: "TaskAssignees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignees_TaskItem_taskId",
                table: "TaskAssignees",
                column: "taskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignees_Users_CreatedById",
                table: "TaskAssignees",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignees_Users_UserId",
                table: "TaskAssignees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignees_TaskItem_taskId",
                table: "TaskAssignees");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignees_Users_CreatedById",
                table: "TaskAssignees");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignees_Users_UserId",
                table: "TaskAssignees");

            migrationBuilder.DropIndex(
                name: "IX_TaskAssignees_CreatedById",
                table: "TaskAssignees");

            migrationBuilder.DropIndex(
                name: "IX_TaskAssignees_taskId",
                table: "TaskAssignees");

            migrationBuilder.DropIndex(
                name: "IX_TaskAssignees_UserId",
                table: "TaskAssignees");

            migrationBuilder.DropColumn(
                name: "AllowComplete",
                table: "TaskAssignees");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "TaskAssignees");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TaskAssignees");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "TaskAssignees");

            migrationBuilder.DropColumn(
                name: "RejectReason",
                table: "TaskAssignees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskAssignees");

            migrationBuilder.DropColumn(
                name: "taskId",
                table: "TaskAssignees");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskAssigneesId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskAssigneesId",
                table: "Department",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskAssigneesId",
                table: "Users",
                column: "TaskAssigneesId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_TaskAssigneesId",
                table: "Department",
                column: "TaskAssigneesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_TaskAssignees_TaskAssigneesId",
                table: "Department",
                column: "TaskAssigneesId",
                principalTable: "TaskAssignees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TaskAssignees_TaskAssigneesId",
                table: "Users",
                column: "TaskAssigneesId",
                principalTable: "TaskAssignees",
                principalColumn: "Id");
        }
    }
}
