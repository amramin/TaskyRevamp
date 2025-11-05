using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdddiffernrDepartmentgeneralModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DelegationDepartments",
                table: "GeneralModulePermission",
                newName: "DelegationToUserDepartments");

            migrationBuilder.AddColumn<string>(
                name: "DelegationFromUserDepartments",
                table: "GeneralModulePermission",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelegationFromUserDepartments",
                table: "GeneralModulePermission");

            migrationBuilder.RenameColumn(
                name: "DelegationToUserDepartments",
                table: "GeneralModulePermission",
                newName: "DelegationDepartments");
        }
    }
}
