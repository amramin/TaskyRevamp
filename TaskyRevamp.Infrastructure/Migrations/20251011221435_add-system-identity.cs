using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsystemidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemIdentity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryActiveColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NavigationBackground = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorderColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemIdentity", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemIdentity");
        }
    }
}
