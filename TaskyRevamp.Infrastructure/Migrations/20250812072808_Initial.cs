using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskAssignees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskChecklist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskChecklist", x => x.Id);
                });

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
                name: "TaskDependencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDependencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UploadedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskAttachmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_TaskAttachments_TaskAttachmentsId",
                        column: x => x.TaskAttachmentsId,
                        principalTable: "TaskAttachments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChangeEndDateRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeEndDateRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskChecklistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                        column: x => x.TaskChecklistId,
                        principalTable: "TaskChecklist",
                        principalColumn: "Id");
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
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskAssigneesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_TaskAssignees_TaskAssigneesId",
                        column: x => x.TaskAssigneesId,
                        principalTable: "TaskAssignees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistinguishedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskAssigneesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_TaskAssignees_TaskAssigneesId",
                        column: x => x.TaskAssigneesId,
                        principalTable: "TaskAssignees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssigneesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DependenciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChecklistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskAssignees_AssigneesId",
                        column: x => x.AssigneesId,
                        principalTable: "TaskAssignees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskAttachments_AttachmentsId",
                        column: x => x.AttachmentsId,
                        principalTable: "TaskAttachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskChecklist_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "TaskChecklist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskComments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "TaskComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskDependencies_DependenciesId",
                        column: x => x.DependenciesId,
                        principalTable: "TaskDependencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskItem_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TaskItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskSource_SourceId",
                        column: x => x.SourceId,
                        principalTable: "TaskSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TaskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItem_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDelegation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateddById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDelegation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDelegation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDelegation_Users_UpdateddById",
                        column: x => x.UpdateddById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskEscalation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EscalatedToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEscalation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEscalation_TaskItem_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEscalation_TaskItem_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskEscalation_Users_EscalatedToId",
                        column: x => x.EscalatedToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskEscalation_Users_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskHistoryEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskHistoryEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskHistoryEntry_TaskItem_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskHistoryEntry_TaskItem_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskHistoryEntry_Users_ById",
                        column: x => x.ById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_TaskAttachmentsId",
                table: "Attachment",
                column: "TaskAttachmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_UploadedById",
                table: "Attachment",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeEndDateRequest_RequesterId",
                table: "ChangeEndDateRequest",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeEndDateRequest_TaskId",
                table: "ChangeEndDateRequest",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeEndDateRequest_TaskItemId",
                table: "ChangeEndDateRequest",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_CreatedById",
                table: "ChecklistItem",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_TaskChecklistId",
                table: "ChecklistItem",
                column: "TaskChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TaskCommentsId",
                table: "Comment",
                column: "TaskCommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_TaskAssigneesId",
                table: "Department",
                column: "TaskAssigneesId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_TaskItemId",
                table: "Department",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEscalation_EscalatedToId",
                table: "TaskEscalation",
                column: "EscalatedToId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEscalation_RequestedById",
                table: "TaskEscalation",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEscalation_TaskId",
                table: "TaskEscalation",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEscalation_TaskItemId",
                table: "TaskEscalation",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistoryEntry_ById",
                table: "TaskHistoryEntry",
                column: "ById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistoryEntry_TaskId",
                table: "TaskHistoryEntry",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistoryEntry_TaskItemId",
                table: "TaskHistoryEntry",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_AssigneesId",
                table: "TaskItem",
                column: "AssigneesId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_AttachmentsId",
                table: "TaskItem",
                column: "AttachmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_ChecklistId",
                table: "TaskItem",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_CommentsId",
                table: "TaskItem",
                column: "CommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_CreatorId",
                table: "TaskItem",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_DependenciesId",
                table: "TaskItem",
                column: "DependenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_ParentId",
                table: "TaskItem",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_SourceId",
                table: "TaskItem",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_TypeId",
                table: "TaskItem",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_CreatedById",
                table: "UserDelegation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_UpdateddById",
                table: "UserDelegation",
                column: "UpdateddById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskAssigneesId",
                table: "Users",
                column: "TaskAssigneesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Users_UploadedById",
                table: "Attachment",
                column: "UploadedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskId",
                table: "ChangeEndDateRequest",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskItemId",
                table: "ChangeEndDateRequest",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_Users_RequesterId",
                table: "ChangeEndDateRequest",
                column: "RequesterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Users_CreatedById",
                table: "ChecklistItem",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_TaskItem_TaskItemId",
                table: "Department",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskAttachments_AttachmentsId",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_CreatorId",
                table: "TaskItem");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "ChangeEndDateRequest");

            migrationBuilder.DropTable(
                name: "ChecklistItem");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "TaskEscalation");

            migrationBuilder.DropTable(
                name: "TaskHistoryEntry");

            migrationBuilder.DropTable(
                name: "UserDelegation");

            migrationBuilder.DropTable(
                name: "TaskAttachments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "TaskItem");

            migrationBuilder.DropTable(
                name: "TaskAssignees");

            migrationBuilder.DropTable(
                name: "TaskChecklist");

            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.DropTable(
                name: "TaskDependencies");

            migrationBuilder.DropTable(
                name: "TaskSource");

            migrationBuilder.DropTable(
                name: "TaskType");
        }
    }
}
