using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class requestnewdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id");
        }
    }
}
