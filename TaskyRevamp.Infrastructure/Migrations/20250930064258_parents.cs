using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class parents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentdepartmentId",
                table: "Department",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentdepartmentId",
                table: "Department",
                column: "ParentdepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Department_ParentdepartmentId",
                table: "Department",
                column: "ParentdepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Department_ParentdepartmentId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_ParentdepartmentId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "ParentdepartmentId",
                table: "Department");
        }
    }
}
