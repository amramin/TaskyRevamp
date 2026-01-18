using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class getchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "IsDeleted",
            //    table: "Type",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "TaskItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsDeleted",
            //    table: "Source",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsDeleted",
            //    table: "PrioritySettings",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "IsDeleted",
            //    table: "Type");

            //migrationBuilder.DropColumn(
            //    name: "IsDeleted",
            //    table: "Source");

            //migrationBuilder.DropColumn(
            //    name: "IsDeleted",
            //    table: "PrioritySettings");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "TaskItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
