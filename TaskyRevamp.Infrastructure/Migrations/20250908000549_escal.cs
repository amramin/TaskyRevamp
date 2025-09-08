using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class escal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEscalation_TaskItem_TaskId",
                table: "TaskEscalation");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskEscalation_Users_RequestedById",
                table: "TaskEscalation");

            migrationBuilder.RenameColumn(
                name: "RequestedById",
                table: "TaskEscalation",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "RequestedAt",
                table: "TaskEscalation",
                newName: "CreateDate");

            migrationBuilder.RenameIndex(
                name: "IX_TaskEscalation_RequestedById",
                table: "TaskEscalation",
                newName: "IX_TaskEscalation_CreatedById");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "TaskEscalation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TriggerAfter",
                table: "TaskEscalation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TriggerStatus",
                table: "TaskEscalation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEscalation_TaskItem_TaskId",
                table: "TaskEscalation",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEscalation_Users_CreatedById",
                table: "TaskEscalation",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEscalation_TaskItem_TaskId",
                table: "TaskEscalation");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskEscalation_Users_CreatedById",
                table: "TaskEscalation");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "TaskEscalation");

            migrationBuilder.DropColumn(
                name: "TriggerAfter",
                table: "TaskEscalation");

            migrationBuilder.DropColumn(
                name: "TriggerStatus",
                table: "TaskEscalation");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TaskEscalation",
                newName: "RequestedById");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "TaskEscalation",
                newName: "RequestedAt");

            migrationBuilder.RenameIndex(
                name: "IX_TaskEscalation_CreatedById",
                table: "TaskEscalation",
                newName: "IX_TaskEscalation_RequestedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEscalation_TaskItem_TaskId",
                table: "TaskEscalation",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEscalation_Users_RequestedById",
                table: "TaskEscalation",
                column: "RequestedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
