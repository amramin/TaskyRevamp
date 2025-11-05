using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migratin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_FromUserId",
                table: "UserDelegation",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_ToUserId",
                table: "UserDelegation",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDelegation_Users_FromUserId",
                table: "UserDelegation",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDelegation_Users_ToUserId",
                table: "UserDelegation",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDelegation_Users_FromUserId",
                table: "UserDelegation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDelegation_Users_ToUserId",
                table: "UserDelegation");

            migrationBuilder.DropIndex(
                name: "IX_UserDelegation_FromUserId",
                table: "UserDelegation");

            migrationBuilder.DropIndex(
                name: "IX_UserDelegation_ToUserId",
                table: "UserDelegation");
        }
    }
}
