using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reportmodulepermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HintEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HintArabic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportModulePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrivilegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportModulePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportModulePermission_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privilege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportModulePermission_ReportModule_ReportModuleId",
                        column: x => x.ReportModuleId,
                        principalTable: "ReportModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportModulePermission_PrivilegeId",
                table: "ReportModulePermission",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportModulePermission_ReportModuleId",
                table: "ReportModulePermission",
                column: "ReportModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportModulePermission");

            migrationBuilder.DropTable(
                name: "ReportModule");
        }
    }
}
