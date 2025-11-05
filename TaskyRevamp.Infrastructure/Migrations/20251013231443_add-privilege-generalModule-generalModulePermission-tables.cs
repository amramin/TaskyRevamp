using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addprivilegegeneralModulegeneralModulePermissiontables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasView = table.Column<bool>(type: "bit", nullable: false),
                    HasEdit = table.Column<bool>(type: "bit", nullable: false),
                    HasAdd = table.Column<bool>(type: "bit", nullable: false),
                    HasDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privilege",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilege", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralModulePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrivilegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsView = table.Column<bool>(type: "bit", nullable: false),
                    IsEdit = table.Column<bool>(type: "bit", nullable: false),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DelegationFromUser = table.Column<int>(type: "int", nullable: true),
                    DelegationToUser = table.Column<int>(type: "int", nullable: true),
                    DelegationDepartments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralModulePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralModulePermission_GeneralModule_GeneralModuleId",
                        column: x => x.GeneralModuleId,
                        principalTable: "GeneralModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralModulePermission_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privilege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralModulePermission_GeneralModuleId",
                table: "GeneralModulePermission",
                column: "GeneralModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralModulePermission_PrivilegeId",
                table: "GeneralModulePermission",
                column: "PrivilegeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralModulePermission");

            migrationBuilder.DropTable(
                name: "GeneralModule");

            migrationBuilder.DropTable(
                name: "Privilege");
        }
    }
}
