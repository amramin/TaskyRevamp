using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddTaskSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddTaskSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultColumnsSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultColumnsSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultViewSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultSelected = table.Column<int>(type: "int", nullable: false),
                    SubTaskLevels = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultViewSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterFieldsSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterFieldsSettings", x => x.Id);
                });

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
                name: "PrioritySettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrioritySettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecycleBinSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodType = table.Column<int>(type: "int", nullable: false),
                    CustomDays = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecycleBinSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RejectionSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodType = table.Column<int>(type: "int", nullable: false),
                    CustomDays = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectionSettings", x => x.Id);
                });

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
                name: "StatusSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusSettings", x => x.Id);
                });

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
                name: "ViewTaskSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewTaskSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyReportSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyReportSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDaysSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDaysSettings", x => x.Id);
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
                    RequesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    TaskChecklistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentdepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Department_ParentdepartmentId",
                        column: x => x.ParentdepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
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
                    DelegationFromUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelegationToUser = table.Column<int>(type: "int", nullable: true),
                    DelegationFromUserDepartments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelegationToUserDepartments = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "PinnedTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinnedTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privilege",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilege", x => x.Id);
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
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrivilegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privilege",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Source_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Source_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Type_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Type_Users_UpdatedById",
                        column: x => x.UpdatedById,
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
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                        name: "FK_UserDelegation_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDelegation_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDelegation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskAssignees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    taskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAssignees_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskAssignees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskChecklist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskItemId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskChecklist", x => x.Id);
                });

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
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskComment_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskComment_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskSourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedDepartmentIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DependenciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AttachmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItem_PrioritySettings_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "PrioritySettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItem_Source_TaskSourceId",
                        column: x => x.TaskSourceId,
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItem_StatusSettings_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskAttachments_AttachmentsId",
                        column: x => x.AttachmentsId,
                        principalTable: "TaskAttachments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskComment_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "TaskComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskDependencies_DependenciesId",
                        column: x => x.DependenciesId,
                        principalTable: "TaskDependencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskItem_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TaskItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskItem_Type_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItem_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItem_Users_UpdatedById",
                        column: x => x.UpdatedById,
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
                    Level = table.Column<int>(type: "int", nullable: false),
                    TriggerAfter = table.Column<int>(type: "int", nullable: false),
                    TriggerStatus = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskEscalation_TaskItem_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskEscalation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskEscalation_Users_EscalatedToId",
                        column: x => x.EscalatedToId,
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
                name: "IX_ChangeEndDateRequest_CreatedById",
                table: "ChangeEndDateRequest",
                column: "CreatedById");

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
                name: "IX_ChecklistItem_AssignedUserId",
                table: "ChecklistItem",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_CreatedById",
                table: "ChecklistItem",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_TaskChecklistId",
                table: "ChecklistItem",
                column: "TaskChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CreatedById",
                table: "Department",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentdepartmentId",
                table: "Department",
                column: "ParentdepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_UpdatedById",
                table: "Department",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralModulePermission_GeneralModuleId",
                table: "GeneralModulePermission",
                column: "GeneralModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralModulePermission_PrivilegeId",
                table: "GeneralModulePermission",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_PinnedTasks_CreatedById",
                table: "PinnedTasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PinnedTasks_TaskId",
                table: "PinnedTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_CreatedById",
                table: "Privilege",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_UpdatedById",
                table: "Privilege",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportModulePermission_PrivilegeId",
                table: "ReportModulePermission",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportModulePermission_ReportModuleId",
                table: "ReportModulePermission",
                column: "ReportModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Source_CreatedById",
                table: "Source",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Source_UpdatedById",
                table: "Source",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignees_CreatedById",
                table: "TaskAssignees",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignees_taskId",
                table: "TaskAssignees",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignees_UserId",
                table: "TaskAssignees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskChecklist_TaskItemId",
                table: "TaskChecklist",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskChecklist_TaskItemId1",
                table: "TaskChecklist",
                column: "TaskItemId1",
                unique: true,
                filter: "[TaskItemId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_CreatedById",
                table: "TaskComment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_TaskItemId",
                table: "TaskComment",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComment_UpdatedById",
                table: "TaskComment",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEscalation_CreatedById",
                table: "TaskEscalation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEscalation_EscalatedToId",
                table: "TaskEscalation",
                column: "EscalatedToId");

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
                name: "IX_TaskItem_AttachmentsId",
                table: "TaskItem",
                column: "AttachmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_CommentsId",
                table: "TaskItem",
                column: "CommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_CreatedById",
                table: "TaskItem",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_DependenciesId",
                table: "TaskItem",
                column: "DependenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_ParentId",
                table: "TaskItem",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_PriorityId",
                table: "TaskItem",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_StatusId",
                table: "TaskItem",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_TaskSourceId",
                table: "TaskItem",
                column: "TaskSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_TaskTypeId",
                table: "TaskItem",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_UpdatedById",
                table: "TaskItem",
                column: "UpdatedById");

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

            migrationBuilder.CreateIndex(
                name: "IX_Type_CreatedById",
                table: "Type",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Type_UpdatedById",
                table: "Type",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_CreatedById",
                table: "UserDelegation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_FromUserId",
                table: "UserDelegation",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_ToUserId",
                table: "UserDelegation",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDelegation_UpdatedById",
                table: "UserDelegation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PrivilegeId",
                table: "Users",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedById",
                table: "Users",
                column: "UpdatedById");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_TaskItem_TaskItemId",
                table: "ChangeEndDateRequest",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_Users_CreatedById",
                table: "ChangeEndDateRequest",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeEndDateRequest_Users_RequesterId",
                table: "ChangeEndDateRequest",
                column: "RequesterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_TaskChecklist_TaskChecklistId",
                table: "ChecklistItem",
                column: "TaskChecklistId",
                principalTable: "TaskChecklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Users_AssignedUserId",
                table: "ChecklistItem",
                column: "AssignedUserId",
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
                name: "FK_Department_Users_CreatedById",
                table: "Department",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Users_UpdatedById",
                table: "Department",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralModulePermission_Privilege_PrivilegeId",
                table: "GeneralModulePermission",
                column: "PrivilegeId",
                principalTable: "Privilege",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PinnedTasks_TaskItem_TaskId",
                table: "PinnedTasks",
                column: "TaskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PinnedTasks_Users_CreatedById",
                table: "PinnedTasks",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Privilege_Users_CreatedById",
                table: "Privilege",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Privilege_Users_UpdatedById",
                table: "Privilege",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignees_TaskItem_taskId",
                table: "TaskAssignees",
                column: "taskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskChecklist_TaskItem_TaskItemId",
                table: "TaskChecklist",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskChecklist_TaskItem_TaskItemId1",
                table: "TaskChecklist",
                column: "TaskItemId1",
                principalTable: "TaskItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComment_TaskItem_TaskItemId",
                table: "TaskComment",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_TaskAttachments_AttachmentsId",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Users_CreatedById",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Users_UpdatedById",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Privilege_Users_CreatedById",
                table: "Privilege");

            migrationBuilder.DropForeignKey(
                name: "FK_Privilege_Users_UpdatedById",
                table: "Privilege");

            migrationBuilder.DropForeignKey(
                name: "FK_Source_Users_CreatedById",
                table: "Source");

            migrationBuilder.DropForeignKey(
                name: "FK_Source_Users_UpdatedById",
                table: "Source");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_Users_CreatedById",
                table: "TaskComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_Users_UpdatedById",
                table: "TaskComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_CreatedById",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Users_UpdatedById",
                table: "TaskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_Users_CreatedById",
                table: "Type");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_Users_UpdatedById",
                table: "Type");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_TaskItem_TaskItemId",
                table: "TaskComment");

            migrationBuilder.DropTable(
                name: "AddTaskSettings");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "ChangeEndDateRequest");

            migrationBuilder.DropTable(
                name: "ChecklistItem");

            migrationBuilder.DropTable(
                name: "DefaultColumnsSettings");

            migrationBuilder.DropTable(
                name: "DefaultViewSettings");

            migrationBuilder.DropTable(
                name: "FilterFieldsSettings");

            migrationBuilder.DropTable(
                name: "GeneralModulePermission");

            migrationBuilder.DropTable(
                name: "PinnedTasks");

            migrationBuilder.DropTable(
                name: "RecycleBinSettings");

            migrationBuilder.DropTable(
                name: "RejectionSettings");

            migrationBuilder.DropTable(
                name: "ReportModulePermission");

            migrationBuilder.DropTable(
                name: "SystemIdentity");

            migrationBuilder.DropTable(
                name: "TaskAssignees");

            migrationBuilder.DropTable(
                name: "TaskEscalation");

            migrationBuilder.DropTable(
                name: "TaskHistoryEntry");

            migrationBuilder.DropTable(
                name: "TaskModuleExternalDepartment");

            migrationBuilder.DropTable(
                name: "TaskModuleUserDepartment");

            migrationBuilder.DropTable(
                name: "UserDelegation");

            migrationBuilder.DropTable(
                name: "ViewTaskSettings");

            migrationBuilder.DropTable(
                name: "WeeklyReportSettings");

            migrationBuilder.DropTable(
                name: "WorkingDaysSettings");

            migrationBuilder.DropTable(
                name: "TaskChecklist");

            migrationBuilder.DropTable(
                name: "GeneralModule");

            migrationBuilder.DropTable(
                name: "ReportModule");

            migrationBuilder.DropTable(
                name: "TaskAttachments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Privilege");

            migrationBuilder.DropTable(
                name: "TaskItem");

            migrationBuilder.DropTable(
                name: "PrioritySettings");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "StatusSettings");

            migrationBuilder.DropTable(
                name: "TaskComment");

            migrationBuilder.DropTable(
                name: "TaskDependencies");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
