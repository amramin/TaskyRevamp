using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class attachmentSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "TaskItem");

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "Attachment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Attachment");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "TaskItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
