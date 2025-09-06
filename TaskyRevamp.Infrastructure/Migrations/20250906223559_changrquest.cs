using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changrquest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ChangeEndDateRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "ChangeEndDateRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsAproved",
                table: "ChangeEndDateRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeEndDateRequest_CreatedById",
                table: "ChangeEndDateRequest",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_Users_CreatedById",
                table: "ChangeEndDateRequest",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeEndDateRequest_Users_CreatedById",
                table: "ChangeEndDateRequest");

            migrationBuilder.DropIndex(
                name: "IX_ChangeEndDateRequest_CreatedById",
                table: "ChangeEndDateRequest");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ChangeEndDateRequest");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ChangeEndDateRequest");

            migrationBuilder.DropColumn(
                name: "IsAproved",
                table: "ChangeEndDateRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
