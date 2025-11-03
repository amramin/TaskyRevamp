using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtaskModuletables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskModuleExternalDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrivilegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsIncludeSubDepartment = table.Column<bool>(type: "bit", nullable: false),
                    IsManagerTasks = table.Column<bool>(type: "bit", nullable: false),
                    IsEmployeeTasks = table.Column<bool>(type: "bit", nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectionType = table.Column<int>(type: "int", nullable: true),
                    DirectionLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModuleExternalDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskModuleExternalDepartment_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModuleExternalDepartment_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privilege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskModuleUserDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrivilegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedOption = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsManagerTasks = table.Column<bool>(type: "bit", nullable: true),
                    IsEmployeeTasks = table.Column<bool>(type: "bit", nullable: true),
                    DirectionType = table.Column<int>(type: "int", nullable: true),
                    DirectionLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModuleUserDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskModuleUserDepartment_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privilege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleExternalDepartment_DepartmentId",
                table: "TaskModuleExternalDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleExternalDepartment_PrivilegeId",
                table: "TaskModuleExternalDepartment",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleUserDepartment_PrivilegeId",
                table: "TaskModuleUserDepartment",
                column: "PrivilegeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModuleExternalDepartment");

            migrationBuilder.DropTable(
                name: "TaskModuleUserDepartment");
        }
    }
}
