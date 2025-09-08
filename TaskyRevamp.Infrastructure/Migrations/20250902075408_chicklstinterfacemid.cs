using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chicklstinterfacemid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskSource_SourceId",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskType_TypeId",
                table: "TaskItem");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "TaskItem",
                newName: "TaskTypeId");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "TaskItem",
                newName: "TaskSourceId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItem_TypeId",
                table: "TaskItem",
                newName: "IX_TaskItem_TaskTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItem_SourceId",
                table: "TaskItem",
                newName: "IX_TaskItem_TaskSourceId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskSource_TaskSourceId",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskType_TaskTypeId",
                table: "TaskItem");

            migrationBuilder.RenameColumn(
                name: "TaskTypeId",
                table: "TaskItem",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "TaskSourceId",
                table: "TaskItem",
                newName: "SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItem_TaskTypeId",
                table: "TaskItem",
                newName: "IX_TaskItem_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItem_TaskSourceId",
                table: "TaskItem",
                newName: "IX_TaskItem_SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskSource_SourceId",
                table: "TaskItem",
                column: "SourceId",
                principalTable: "TaskSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskType_TypeId",
                table: "TaskItem",
                column: "TypeId",
                principalTable: "TaskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
