using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adddateinprivilegetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Privilege",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Privilege",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Privilege",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Privilege",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_CreatedById",
                table: "Privilege",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_UpdatedById",
                table: "Privilege",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Privilege_Users_CreatedById",
                table: "Privilege",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Privilege_Users_UpdatedById",
                table: "Privilege",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Privilege_Users_CreatedById",
                table: "Privilege");

            migrationBuilder.DropForeignKey(
                name: "FK_Privilege_Users_UpdatedById",
                table: "Privilege");

            migrationBuilder.DropIndex(
                name: "IX_Privilege_CreatedById",
                table: "Privilege");

            migrationBuilder.DropIndex(
                name: "IX_Privilege_UpdatedById",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Privilege");
        }
    }
}
