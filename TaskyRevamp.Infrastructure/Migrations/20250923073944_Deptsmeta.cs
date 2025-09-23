using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Deptsmeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Department",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Department",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Department",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Department",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_CreatedById",
                table: "Department",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Department_UpdatedById",
                table: "Department",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Users_CreatedById",
                table: "Department",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Users_UpdatedById",
                table: "Department",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Users_CreatedById",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Users_UpdatedById",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_CreatedById",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_UpdatedById",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Department");
        }
    }
}
