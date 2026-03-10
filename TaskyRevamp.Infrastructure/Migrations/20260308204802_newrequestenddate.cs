using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newrequestenddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop FK if exists
            migrationBuilder.Sql(@"
    IF EXISTS (
        SELECT * FROM sys.foreign_keys 
        WHERE name = 'FK_ChangeEndDateRequest_TaskItem_TaskItemId'
    )
    ALTER TABLE ChangeEndDateRequest
    DROP CONSTRAINT FK_ChangeEndDateRequest_TaskItem_TaskItemId
    ");

            // Drop old TaskId FK
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest");

            // Drop old TaskId index
            migrationBuilder.DropIndex(
                name: "IX_ChangeEndDateRequest_TaskId",
                table: "ChangeEndDateRequest");

            // Drop old TaskItemId index if exists
            migrationBuilder.Sql(@"
    IF EXISTS (
        SELECT name 
        FROM sys.indexes 
        WHERE name = 'IX_ChangeEndDateRequest_TaskItemId' 
          AND object_id = OBJECT_ID('ChangeEndDateRequest')
    )
    DROP INDEX IX_ChangeEndDateRequest_TaskItemId ON ChangeEndDateRequest
    ");

            // Drop old column
            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "ChangeEndDateRequest");

            // Alter TaskItemId
            migrationBuilder.AlterColumn<Guid>(
                name: "TaskItemId",
                table: "ChangeEndDateRequest",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            // Recreate index
            migrationBuilder.CreateIndex(
                name: "IX_ChangeEndDateRequest_TaskItemId",
                table: "ChangeEndDateRequest",
                column: "TaskItemId");

            // Recreate FK
            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskItemId",
                table: "ChangeEndDateRequest",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskItemId",
                table: "ChangeEndDateRequest");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskItemId",
                table: "ChangeEndDateRequest",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "ChangeEndDateRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChangeEndDateRequest_TaskId",
                table: "ChangeEndDateRequest",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskItemId",
                table: "ChangeEndDateRequest",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");
        }
    }
}
