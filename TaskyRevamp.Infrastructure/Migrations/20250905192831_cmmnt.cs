using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cmmnt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskComments_CommentsId",
                table: "TaskItem");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.CreateTable(
                name: "TaskComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateddById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskComment_TaskItem_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskComment_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskComment_Users_UpdateddById",
                        column: x => x.UpdateddById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_CreatedById",
                table: "TaskComment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_TaskItemId",
                table: "TaskComment",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_UpdateddById",
                table: "TaskComment",
                column: "UpdateddById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskComment_CommentsId",
                table: "TaskItem",
                column: "CommentsId",
                principalTable: "TaskComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskComment_CommentsId",
                table: "TaskItem");

            migrationBuilder.DropTable(
                name: "TaskComment");

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskCommentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_TaskComments_TaskCommentsId",
                        column: x => x.TaskCommentsId,
                        principalTable: "TaskComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TaskCommentsId",
                table: "Comment",
                column: "TaskCommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskComments_CommentsId",
                table: "TaskItem",
                column: "CommentsId",
                principalTable: "TaskComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
