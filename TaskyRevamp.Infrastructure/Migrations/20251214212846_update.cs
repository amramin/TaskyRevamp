using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskyRevamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Attachment_TaskAttachments_TaskAttachmentsId",
            //    table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Users_UploadedById",
                table: "Attachment");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TaskItem_TaskAttachments_AttachmentsId",
            //    table: "TaskItem");

            //migrationBuilder.DropIndex(
            //    name: "IX_TaskItem_AttachmentsId",
            //    table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_TaskAttachmentsId",
                table: "Attachment");

            //migrationBuilder.DropColumn(
            //    name: "AttachmentsId",
            //    table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "TaskAttachmentsId",
                table: "Attachment");

            migrationBuilder.RenameColumn(
                name: "UploadedById",
                table: "Attachment",
                newName: "TaskAttachmentId");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "Attachment",
                newName: "CreateDate");

            migrationBuilder.RenameIndex(
                name: "IX_Attachment_UploadedById",
                table: "Attachment",
                newName: "IX_Attachment_TaskAttachmentId");

            //migrationBuilder.AddColumn<int>(
            //    name: "Weight",
            //    table: "TaskItem",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "TaskItemId",
            //    table: "TaskAttachments",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<long>(
                name: "SubTaskLevels",
                table: "DefaultViewSettings",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Attachment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_TaskItemId",
                table: "TaskAttachments",
                column: "TaskItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_CreatedById",
                table: "Attachment",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_TaskAttachments_TaskAttachmentId",
                table: "Attachment",
                column: "TaskAttachmentId",
                principalTable: "TaskAttachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Users_CreatedById",
                table: "Attachment",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAttachments_TaskItem_TaskItemId",
                table: "TaskAttachments",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_TaskAttachments_TaskAttachmentId",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Users_CreatedById",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAttachments_TaskItem_TaskItemId",
                table: "TaskAttachments");

            migrationBuilder.DropIndex(
                name: "IX_TaskAttachments_TaskItemId",
                table: "TaskAttachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_CreatedById",
                table: "Attachment");

            //migrationBuilder.DropColumn(
            //    name: "Weight",
            //    table: "TaskItem");

            //migrationBuilder.DropColumn(
            //    name: "TaskItemId",
            //    table: "TaskAttachments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Attachment");

            migrationBuilder.RenameColumn(
                name: "TaskAttachmentId",
                table: "Attachment",
                newName: "UploadedById");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Attachment",
                newName: "UploadedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Attachment_TaskAttachmentId",
                table: "Attachment",
                newName: "IX_Attachment_UploadedById");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentsId",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubTaskLevels",
                table: "DefaultViewSettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskAttachmentsId",
                table: "Attachment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_AttachmentsId",
                table: "TaskItem",
                column: "AttachmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_TaskAttachmentsId",
                table: "Attachment",
                column: "TaskAttachmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_TaskAttachments_TaskAttachmentsId",
                table: "Attachment",
                column: "TaskAttachmentsId",
                principalTable: "TaskAttachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Users_UploadedById",
                table: "Attachment",
                column: "UploadedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_TaskAttachments_AttachmentsId",
                table: "TaskItem",
                column: "AttachmentsId",
                principalTable: "TaskAttachments",
                principalColumn: "Id");
        }
    }
}
