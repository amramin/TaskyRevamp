using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateupdatebycolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_UpdateddById",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDelegation_Users_UpdateddById",
                table: "UserDelegation");

            migrationBuilder.DropIndex(
                name: "IX_UserDelegation_UpdateddById",
                table: "UserDelegation");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_UpdateddById",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "UpdateddById",
                table: "UserDelegation");

            migrationBuilder.DropColumn(
                name: "UpdateddById",
                table: "TaskItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedById",
                table: "Source",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_UpdatedById",
                table: "UserDelegation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_UpdatedById",
                table: "TaskItem",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Users_UpdatedById",
                table: "TaskItem",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDelegation_Users_UpdatedById",
                table: "UserDelegation",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_UpdatedById",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDelegation_Users_UpdatedById",
                table: "UserDelegation");

            migrationBuilder.DropIndex(
                name: "IX_UserDelegation_UpdatedById",
                table: "UserDelegation");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_UpdatedById",
                table: "TaskItem");

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateddById",
                table: "UserDelegation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateddById",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedById",
                table: "Source",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_UpdateddById",
                table: "UserDelegation",
                column: "UpdateddById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_UpdateddById",
                table: "TaskItem",
                column: "UpdateddById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Users_UpdateddById",
                table: "TaskItem",
                column: "UpdateddById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDelegation_Users_UpdateddById",
                table: "UserDelegation",
                column: "UpdateddById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
