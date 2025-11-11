using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fkkkm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskSource_TaskSourceId",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskType_TaskTypeId",
                table: "TaskItem");

            migrationBuilder.DropTable(
                name: "TaskSource");

            migrationBuilder.DropTable(
                name: "TaskType");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TaskItem");

            migrationBuilder.AddColumn<Guid>(
                name: "PriorityId",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_PriorityId",
                table: "TaskItem",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_PrioritySettings_PriorityId",
                table: "TaskItem",
                column: "PriorityId",
                principalTable: "PrioritySettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Source_TaskSourceId",
                table: "TaskItem",
                column: "TaskSourceId",
                principalTable: "Source",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Type_TaskTypeId",
                table: "TaskItem",
                column: "TaskTypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_PrioritySettings_PriorityId",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Source_TaskSourceId",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Type_TaskTypeId",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_PriorityId",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "TaskItem");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "TaskItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TaskSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskType", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskSource_TaskSourceId",
                table: "TaskItem",
                column: "TaskSourceId",
                principalTable: "TaskSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskType_TaskTypeId",
                table: "TaskItem",
                column: "TaskTypeId",
                principalTable: "TaskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
