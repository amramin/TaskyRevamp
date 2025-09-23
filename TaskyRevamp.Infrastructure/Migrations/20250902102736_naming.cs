using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class naming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskDependencies_DependenciesId",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_CreatorId",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_CreatorId",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TaskItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "DependenciesId",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskDependencies_DependenciesId",
                table: "TaskItem",
                column: "DependenciesId",
                principalTable: "TaskDependencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskDependencies_DependenciesId",
                table: "TaskItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "DependenciesId",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_CreatorId",
                table: "TaskItem",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskDependencies_DependenciesId",
                table: "TaskItem",
                column: "DependenciesId",
                principalTable: "TaskDependencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Users_CreatorId",
                table: "TaskItem",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
