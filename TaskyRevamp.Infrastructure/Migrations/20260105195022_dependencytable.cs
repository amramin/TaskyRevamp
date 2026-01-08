using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dependencytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_TaskItem_TaskDependencies_DependenciesId",
            //    table: "TaskItem");

            //migrationBuilder.DropIndex(
            //    name: "IX_TaskItem_DependenciesId",
            //    table: "TaskItem");

            //migrationBuilder.DropColumn(
            //    name: "DependenciesId",
            //    table: "TaskItem");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDependencies_TaskItemId",
                table: "TaskDependencies",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_TaskItem_TaskItemId",
                table: "TaskDependencies",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_TaskItem_TaskItemId",
                table: "TaskDependencies");

            migrationBuilder.DropIndex(
                name: "IX_TaskDependencies_TaskItemId",
                table: "TaskDependencies");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "DependenciesId",
            //    table: "TaskItem",
            //    type: "uniqueidentifier",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_TaskItem_DependenciesId",
            //    table: "TaskItem",
            //    column: "DependenciesId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TaskItem_TaskDependencies_DependenciesId",
            //    table: "TaskItem",
            //    column: "DependenciesId",
            //    principalTable: "TaskDependencies",
            //    principalColumn: "Id");
        }
    }
}
