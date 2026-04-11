using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Notification;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Infrastructure;

namespace TaskyRevamp.Infrastructure.Seeders
{
    public class NotificationTypeTemplateSeeder : ISeeder
    {
        public void Seed(EfDbContext context)
        {
            var NotificationTypeTemplatesToSeed = new[]
            {

               #region System Notification
		        new NotificationTypeTemplate
                {
                    NameEnglish="Task creation",
                    NameArabic="إنشاء مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker] assigned a task to you:&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">قام <span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker]&nbsp;</span>بتكليفك بمهمة: <span style=\"color: rgb(0, 0, 0); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Task title]</span>.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Change task progress",
                    NameArabic="تغيير تقدم مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker] updated the progress of&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker]</span> بتحديث نسبة إنجاز المهمة <span style=\"font-family: Indivisible; color: rgb(33, 37, 41); font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">&nbsp;</span><span style=\"font-family: Indivisible; color: rgb(33, 37, 41); font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-family: Indivisible;\">[Task title]</span></span>.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reopen task",
                    NameArabic="إعادة فتح مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">&nbsp;</b><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>returned the task&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title] for revision.</span></span></p>",
                    TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">قام <span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker]</span><span style=\"font-weight: 400;\">&nbsp;</span>بإرجاع المهمة<span style=\"font-weight: 400;\">&nbsp;<span style=\"font-family: Indivisible; color: rgb(33, 37, 41); font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">&nbsp;</span><span style=\"font-family: Indivisible; color: rgb(33, 37, 41); font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-family: Indivisible;\">[Task title]</span></span></span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">للمراجعة.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Complete task",
                    NameArabic="إكمال مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>completed the task&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">قام<span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker]</span> بإكمال المهمة <span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Task title].</span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delayed task",
                    NameArabic="مهمة متأخرة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">The task&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span><span>[Task title]</span></span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\"><span>&nbsp;</span>has been delayed. Please review it urgently to ensure completion.</span></p>",
                    TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">.تم تأخير المهمة&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Task title]</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">. يرجى مراجعتها بشكل عاجل لضمان إكمالها</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Pending review",
                    NameArabic="في انتظار المراجعة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">The task&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span><span>[Task title]</span></span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\"><span>&nbsp;</span>is ready for review. Please review it to proceed with completion.</span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">إن المهمة<span style=\"color: rgb(0, 0, 0); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Task title]</span></span><span style=\"font-weight: 400; display: inline !important;\"> جاهزة للمراجعة. يرجى مراجعتها لاستكمالها.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Request end date change",
                    NameArabic="طلب تغيير تاريخ الانتهاء",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>requested to change the end date of&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title] to [requested new date].</span></span></p>",
                    TemplateArabic="<p style=\"text-align: start;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">&nbsp;</b><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\"><span><span style=\"display: inline !important;\"><span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[requested new date]</span>&nbsp;</span></span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">قام <span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بطلب تغيير تاريخ انتهاء المهمة<span>&nbsp;<span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Task title]</span></span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\"> إلى .</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Approve the end date change request",
                    NameArabic="الموافقة على طلب تغيير تاريخ الانتهاء",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;[Action -&nbsp;</span>approved] the end date change request for&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">قام <span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker]&nbsp;</span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-weight: 400;\">&nbsp;ب&nbsp;</span><span style=\"font-family: Indivisible; color: rgb(33, 37, 41); font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">[Action -&nbsp;</span><span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">approved]</span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">على طلب تغيير تاريخ انتهاء المهمة</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">&nbsp;</span><span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Task title].</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reject the end date change request",
                    NameArabic="رفض طلب تغيير تاريخ الانتهاء",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">[Action taker]<span>&nbsp;[Action -&nbsp;</span>rejected] the end date change request for&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\"><span>قام<span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker]</span></span><span><span>&nbsp;ب</span><span style=\"font-family: Indivisible; color: rgb(0, 0, 0); font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal;\">&nbsp;[Action -&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">rejected]&nbsp;</span></span><span>على طلب تغيير تاريخ انتهاء المهمة</span><span>&nbsp;<span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Task title].</span></span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Send support request",
                    NameArabic="ارسال طلب مساعده",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>sent a support request regarding&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بإرسال طلب دعم بخصوص المهمة<span>&nbsp;</span><span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Task title]</span>.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit task",
                    NameArabic="تعديل مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>updated the details of&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بتحديث تفاصيل المهمة<span>&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reject task",
                    NameArabic="رفض مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">&nbsp;</b><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>rejected the task&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام<span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>برفض المهمة<span>&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Change task priority",
                    NameArabic="تغيير أولوية مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>changed the priority of&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title] to [priority].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بتغيير أولوية المهمة<span>&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title]</span></span> إلى&nbsp;</span><span style=\"font-family: Almarai, sans-serif; direction: rtl; font-weight: 400; display: inline !important;\"><span style=\"font-family: Almarai, sans-serif; direction: rtl;\"><span style=\"font-family: Almarai, sans-serif; direction: rtl; display: inline !important;\"><span style=\"font-weight: 400;\"><span><span>&nbsp;</span>[priority]</span></span>.</span></span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Escalate task",
                    NameArabic="تصعيد مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">&nbsp;</b><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>escalated the task&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام&nbsp;<span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بتصعيد المهمة<span>&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title]</span></span></span><span style=\"font-weight: 400; display: inline !important;\">.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete task",
                    NameArabic=" حذف مهمة",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]<span>&nbsp;</span>deleted the task&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]&nbsp;</span><span>&nbsp;</span>بحذف المهمة<span>&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title]</span></span>.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Mention user in comment",
                    NameArabic="الإشارة إلى المستخدم في التعليق",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]&nbsp;mentioned you in a comment on&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span></b></b></b></b></b></b></b></b></b><span>&nbsp;</span>بالإشارة إليك في تعليق على المهمة&nbsp;<b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><span style=\"font-weight: 400;\"><span>[Task title]</span></span></b></b></b></b></b></b></b></b></b>.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Add delegation",
                    NameArabic="إضافة تفويض",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]&nbsp;added a delegation to you&nbsp;</span><span style=\"font-weight: 400;\"><span>[delegated user].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بإضافة تفويض إليك <span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[delegated user].</span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete delegation",
                    NameArabic="حذف تفويض",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]&nbsp;removed a delegation added to you&nbsp;</span><span style=\"font-weight: 400;\"><span>[delegated user].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بحذف التفويض المضاف إليك<span>&nbsp;<span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[delegated user].</span></span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit delegation",
                    NameArabic="تعديل تفويض",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]&nbsp;updated a delegation added to you&nbsp;</span><span style=\"font-weight: 400;\"><span>[delegated user].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بتحديث التفويض المضاف إليك<span>&nbsp;<span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[delegated user]</span></span></span><span style=\"font-weight: 400; display: inline !important;\">.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Add escalation",
                    NameArabic="إضافة تصعيد",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span><span><span><span><span><span><span><span><span><span><span><span style=\"display: inline !important;\"><span style=\"font-weight: 400;\">[Action taker] escalated<span>&nbsp;</span></span><span><span><span><span><span><span><span><span><span><span><span><span><span><span><span>[Task title]</span></span></span></span></span></span></span></span></span></span></span></span></span></span></span><span style=\"font-weight: 400;\"><span>&nbsp;</span>to you for your support</span></span><span style=\"font-weight: 400;\"><span>.</span></span></span></span></span></span></span></span></span></span></span></span></span></span></p>",
                    TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span>&nbsp;بتصعيد&nbsp;</span><span style=\"font-weight: 400;\"><span>[Task title]</span></span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-weight: 400;\">&nbsp;</span></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">إليك للحصول على دعمك.</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete escalation",
                    NameArabic="حذف تصعيد",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]&nbsp;removed the escalation added to you on&nbsp;</span><span style=\"font-weight: 400;\"><span>[<span style=\"display: inline !important;\">Task title</span>].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]</span><span>&nbsp;</span>بحذف التصعيد المضاف إليك على&nbsp;<span style=\"font-weight: 400;\"><span>[Task title].</span></span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit escalation",
                    NameArabic="تعديل تصعيد",
                    IsEnable=true,
                    moduleType=Dto.Enums.ModuleType.SystemNotification,
                    TemplateEnglish="<p><span style=\"font-weight: 400; display: inline !important;\">[Action taker]&nbsp;updated the<span>&nbsp;</span>escalation<span>&nbsp;</span>added to you on&nbsp;</span><span style=\"font-weight: 400;\"><span>[<span style=\"display: inline !important;\">Task title</span>].</span></span></p>",
                    TemplateArabic="<p><span style=\"font-weight: 400; display: inline !important;\">قام <span style=\"font-weight: 400; display: inline !important;\">[Action taker]&nbsp;</span><span>&nbsp;</span>بتحديث تفاصيل التصعيد المضاف إليك على&nbsp;<span style=\"font-weight: 400;\"><span>[Task title].</span></span></span></p>"
                }, 
	#endregion

               #region Email Notifications
		        new NotificationTypeTemplate
                {
                    NameEnglish="Task creation",
                    NameArabic="إنشاء مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="New task created – [Task Name]",
                    SubjectArabic="[Task Name] – تم إنشاء مهمة جديدة",
                    TemplateEnglish="<p><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">Dear [Recipient Name],</span><br style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><br style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">A new task [Task Name] has been created and assigned to you.<br>Please review the details and proceed accordingly.<br><br>Regards,</p>",
                    TemplateArabic="<p><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">[اسم المستلم]<br></span><br style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">تم إنشاء مهمة جديدة [اسم المهمة] وتم تعيينها إليك.<br></p><p>يرجى مراجعة التفاصيل والمتابعة وفقًا لذلك.</p><p>مع التحية.</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Change task progress",
                    NameArabic="تغيير تقدم مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task progress updated – [Task Name]",
                    SubjectArabic="[Task Name] – تم تحديث تقدم المهمة",
                    TemplateEnglish="<p>Dear [Recipient Name],</p><p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">The task [Task Name] has been updated.</span></p><p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\"></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">New progress: [Progress]%</span></p><p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">Regards,</span><br></p>",
                    TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">[اسم المستلم]</span></p><p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">تم تحديث مهمة [اسم المهمة.</span></p><p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">%نسبة التقدم الجديدة&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[التقدم].</span></span></p><p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">مع التحية.</span></span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reopen task",
                    NameArabic="إعادة فتح مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task reopened – [Task Name]",
                    SubjectArabic="[Task Name] – تم إعادة المهمة",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><br>The task [Task Name] has been reopened for further updates.<br><span style=\"display: inline !important;\">Please review the details and proceed accordingly.</span></p><p><span style=\"display: inline !important;\"></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>.تمت إعادة مهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة] لإجراء تحديثات إضافية.</span><br></span>يرجى مراجعة التفاصيل والمتابعة وفقًا لذلك.</p><p><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Complete task",
                    NameArabic="اكمال مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task completed – [Task Name]",
                    SubjectArabic="[Task Name] – تم إكمال المهمة",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><br><span style=\"display: inline !important;\">The task [Task Name] has been marked as completed.</span></p><p><span style=\"display: inline !important;\"></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم وضع علامة \"مكتملة\" على مهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].</span><br></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delayed task",
                    NameArabic="مهمة متأخرة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task delayed – [Task Name]",
                    SubjectArabic="[Task Name] – مهمة متأخرة",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><br><span style=\"display: inline !important;\">The task [Task Name] has been marked as delayed.</span></p><p><span style=\"display: inline !important;\"></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم وضع علامة \"متأخرة\" على مهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].</span><br></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Pending review",
                    NameArabic="في انتظار المراجعة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task ready for review – [Task Name]",
                    SubjectArabic="[Task Name] – مهمة جاهزة للمراجعة",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><br><span style=\"display: inline !important;\">The task [Task Name] is now ready for your review.</span></p><p><span style=\"display: inline !important;\"></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span><span style=\"display: inline !important;\">إن المهمة&nbsp;</span><span style=\"display: inline !important;\">[اسم المهمة] جاهزة الآن للمراجعة.</span><br></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Request end date change",
                    NameArabic="طلب تغيير تاريخ الانتهاء",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="End date change requested – [Task Name]",
                    SubjectArabic="[Task Name] – طلب تغيير تاريخ الانتهاء",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><span style=\"display: inline !important;\"><br><span>A request to change the end date for [Task Name] has been submitted.<br></span><span>Requested end date: [Requested End Date].</span></span></p><p><span style=\"display: inline !important;\"><span></span></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم تقديم طلب لتغيير تاريخ الانتهاء للمهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].<br></span><span>تاريخ الانتهاء المطلوب&nbsp;<span style=\"display: inline !important;\">[تاريخ انتهاء الطلب].</span></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Approve the end date change request",
                    NameArabic="الموافقة على طلب تغيير تاريخ الانتهاء",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Approve the end date change – [Task Name]",
                    SubjectArabic="[Task Name] – الموافقة على تغيير تاريخ الانتهاء",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><span style=\"display: inline !important;\"><span><br><span>Your request to change the end date for [Task Name] has been Approved.<br></span><span>New end date: [New End Date].</span><br></span></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم قبول طلب تغيير تاريخ الانتهاء للمهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].<br></span><span>تاريخ الانتهاء الجديد&nbsp;<span style=\"display: inline !important;\">[تاريخ الانتهاء الجديد].</span></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reject the end date change request",
                    NameArabic="رفض طلب تغيير تاريخ الانتهاء",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Reject the change decision – [Task Name]",
                    SubjectArabic="[Task Name] – رفض تغيير تاريخ الانتهاء",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><span style=\"display: inline !important;\"><span><br><span>Your request to change the end date for [Task Name] has been Rejected.<br></span><span>New end date: [New End Date].</span><br></span></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم رفض طلب تغيير تاريخ الانتهاء للمهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].<br></span><span>تاريخ الانتهاء الجديد&nbsp;<span style=\"display: inline !important;\">[تاريخ الانتهاء الجديد].</span></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Send support request",
                    NameArabic="تقديم طلب دعم",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Support request submitted – [Task Name]",
                    SubjectArabic="[Task Name] – تم تقديم طلب دعم",
                    TemplateEnglish="<p>Dear [Recipient Name],</p><p>A support request for task [Task Name] has been submitted.</p><p style=\"text-align: start;\"><span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">Regards,</span></p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم تقديم طلب دعم للمهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].<br></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit task",
                    NameArabic="تعديل مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task updated – [Task Name]",
                    SubjectArabic="[Task Name] – تم تحديث المهمة",
                    TemplateEnglish="<p>Dear [Recipient Name],</p><p>The task [Task Name] has been updated.</p><p style=\"text-align: start;\"><span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">Regards,</span></p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>.تم تحديث المهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].<br></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reject task",
                    NameArabic="رفض مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task rejected – [Task Name]",
                    SubjectArabic="[Task Name] – تم رفض المهمة",
                    TemplateEnglish="<p><span>Dear [Recipient Name],</span><br><br><span>The ta</span>sk [Task Name] h<span>as been rejected.</span><br></p><p>Please review the details and proceed accordingly.</p><p>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">ع</span><span style=\"display: inline !important;\">زيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span>تم رفض مهمة [اسم المهمة].</p><p>يرجى مراجعة التفاصيل والمتابعة وفقًا لذلك.</p><p><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Change task priority",
                    NameArabic="تغيير أولوية مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task Priority Updated – [Task Name]",
                    SubjectArabic="[Task Name] – تم تحديث أولوية المهمة",
                    TemplateEnglish="<p>Dear [Recipient Name],<br class=\"pasteContent_RTE\"></p><p>The priority of task [Task Name] has been updated.</p><p>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم<span>&nbsp;</span>تحديث أولوية مهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].<br></span></span><br>مع التحية,</p>",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Escalate task",
                    NameArabic="تصعيد مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task Escalated – [Task Name]",
                    SubjectArabic="[Task Name] – تم تصعيد المهمة",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><br>The task [Task Name] has been escalated due to unresolved issues.<br></p><p>Escalation level: [Escalation_Level].</p><p>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم تصعيد مهمة [<span style=\"display: inline !important;\">اسم المهمة</span>] بسبب وجود مشكلات لم يتم حلها.<br><span>مستوى التصعيد:&nbsp;<span style=\"display: inline !important;\">[مستوى التصعيد].</span></span></span></p><p><span><span><span style=\"display: inline !important;\"></span></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete task",
                    NameArabic="حذف مهمة",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Task deleted– [Task Name]",
                    SubjectArabic="[Task Name] – تم حذف المهمة",
                    TemplateEnglish="<p>Dear [Recipient Name],</p><p>The task [Task Name] has been deleted.</p><p>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم حذف مهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].<br></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Mention user in comment",
                    NameArabic="الإشارة إلى المستخدم في التعليق",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="You were mentioned in a comment – [Task Name]",
                    SubjectArabic="[Task Name] – تم ذكرك في تعليق",
                    TemplateEnglish="<p>Dear [Recipient Name],<br></p><p>You were mentioned in a comment on task [Task Name].</p><p>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تمت الإشارة إليك في تعليق على المهمة&nbsp;<span style=\"display: inline !important;\">[اسم المهمة].<br></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Weekly report",
                    NameArabic="التقرير الأسبوعي",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Weekly task report",
                    SubjectArabic="التقرير الأسبوعي للمهام",
                    TemplateEnglish="<p>Dear [Recipient Name],<br></p><p>Please find your weekly task summary in the system.</p><p>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>يرجى الاطلاع على ملخص مهامك الأسبوعي داخل النظام.<span style=\"display: inline !important;\"><br></span></span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Add delegation",
                    NameArabic="إضافة تفويض",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Delegation added",
                    SubjectArabic="تم إضافة تفويض",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><br>A new delegation has been added to your account.<br></p><p>Delegated from: [Delegated from name].</p><p>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تمت إضافة تفويض جديد إلى حسابك.<span style=\"display: inline !important;\"><br></span></span>المفوّض من: [اسم المفوّض].<br><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete delegation",
                    NameArabic="حذف تفويض",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Delegation removed",
                    SubjectArabic="تم حذف التفويض",
                    TemplateEnglish="<p><em></em>Dear [Recipient Name],<br><br>A delegation has been removed from your account.<br><span>Delagated from: [Delegated from name].</span></p><p><span></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]</span></p><p><span style=\"display: inline !important;\"><br></span></p><p><span>تم حذف أحد التفويضات من حسابك.<span style=\"display: inline !important;\"><br></span></span>المفوّض من: [اسم المفوّض].<br><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit delegation",
                    NameArabic="تعديل تفويض",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Delegation updated",
                    SubjectArabic="تم تعديل التفويض",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><br>A delegation linked to your account has been updated.<br><span>Delegated from: [Delegated from name].</span></p><p><span></span>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br><span>تم تعديل أحد التفويضات المرتبطة بحسابك.<span style=\"display: inline !important;\"><br></span></span>المفوّض من: [اسم المفوّض].<br><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Add escalation",
                    NameArabic="إضافة تصعيد",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Escalation added",
                    SubjectArabic="تم إضافة تصعيد",
                    TemplateEnglish="<p>Dear [Recipient Name],<br><br>There is an issue within [Task Name] that requires immediate escalation. Despite our efforts to resolve the matter at the team level, it remains unresolved and is impacting progress.<br>Escalation level: [Escalation_Level]<br></p><p>A new escalation has been added for task [Task Name].</p><p>Regards,</p>",
                    TemplateArabic="<p><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"display: inline !important;\">[اسم المستلم]<br></span><br>توجد مشكلة داخل [اسم المهمة] وتتطلب التصعيد الفوري. على الرغم من جهودنا لحل المشكلة على مستوى الفريق، إلا أنها لا تزال دون حل وتؤثر على التقدم.&nbsp;&nbsp;<br>مستوى التصعيد: <span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">[Escalation_Level].</span><br>مع التحية,</p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete escalation",
                    NameArabic="حذف تصعيد",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Escalation removed",
                    SubjectArabic="تم حذف التصعيد",
                    TemplateEnglish="<p><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">Dear [Recipient's Name],</span></p><p><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\"></span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">An escalation for task [Task Name] has been removed.</span><br>Regards,</p>",
                    TemplateArabic="<p><span><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span>[اسم المستلم]&nbsp; ,<br></span><br><span style=\"display: inline !important;\">تم حذف تصعيد خاص بالمهمة&nbsp;[<span style=\"display: inline !important;\">اسم المهمة</span>].</span><br><span style=\"display: inline !important;\">مع التحية,</span></p>"
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit escalation",
                    NameArabic="تعديل تصعيد",
                    IsEnable=false,
                    moduleType=Dto.Enums.ModuleType.Email,
                    SubjectEnglish="Escalation updated",
                    SubjectArabic="تم تعديل التصعيد",
                    TemplateEnglish="<p><span style=\"display: inline !important;\">Dear [Recipient's Name],</span></p><p><span style=\"display: inline !important;\">​</span><span style=\"display: inline !important;\">An escalation for task [Task Name] has been updated.</span><br>Regards,<span>&nbsp;</span></p>",
                    TemplateArabic="<p><span><span style=\"display: inline !important;\">عزيزي/عزيزتي&nbsp;</span>[اسم المستلم]&nbsp; ,<br></span><br><span style=\"display: inline !important;\">تم تعديل تصعيد خاص بالمهمة&nbsp;[<span style=\"display: inline !important;\">اسم المهمة</span>].</span><br><span style=\"display: inline !important;\">مع التحية</span><b><span style=\"display: inline !important;\">,</span></b></p>"
                },
                #endregion

                new NotificationTypeTemplate
                {
                    NameEnglish="Task creation",
                    NameArabic="إنشاء مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Change task progress",
                    NameArabic="تغيير تقدم مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reopen task",
                    NameArabic="إعادة فتح مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Complete task",
                    NameArabic="اكمال المهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delayed task",
                    NameArabic="مهمة متأخرة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Pending review",
                    NameArabic="في انتظار المراجعة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Add attachment",
                    NameArabic="إضافة ملف",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete attachment",
                    NameArabic="حذف ملف",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Request end date change",
                    NameArabic="طلب تغيير تاريخ الانتهاء",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Approve the end date change request",
                    NameArabic="الموافقة على طلب تغيير تاريخ الانتهاء",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reject the end date change request",
                    NameArabic="رفض طلب تغيير تاريخ الانتهاء",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit task",
                    NameArabic="تعديل مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Reject task",
                    NameArabic="رفض مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Change task priority",
                    NameArabic="تغيير أولوية مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit task description",
                    NameArabic="تعديل وصف مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Add a task checklist",
                    NameArabic="إضافة قائمة تحقق للمهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete the task checklist",
                    NameArabic="حذف قائمة تحقق المهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Update the task checklist",
                    NameArabic="تحديث قائمة تحقق المهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Escalate task",
                    NameArabic="تصعيد مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Add comment",
                    NameArabic="إضافة تعليق",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete comment",
                    NameArabic="حذف تعليق",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete task",
                    NameArabic="حذف مهمة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Restore the deleted task",
                    NameArabic="استعادة مهمة محذوفة",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Mention user in the comment",
                    NameArabic="ذكر اسم المستخدم في التعليق",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Add Escalation",
                    NameArabic="إضافة تصعيد",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Delete Escalation",
                    NameArabic="حذف تصعيد",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                },
                new NotificationTypeTemplate
                {
                    NameEnglish="Edit Escalation",
                    NameArabic="تعديل تصعيد",
                    IsEnable=true,
                    moduleType= Dto.Enums.ModuleType.History,
                    TemplateEnglish="",
                    TemplateArabic="",
                }
            };
            //var NotificationTypeTemplatesToSeed = new[]
            //{
            //    new NotificationTypeTemplate{
            //       NameEnglish = "Task creation",
            //       IsEnable=false,
            //       NameArabic = "إنشاء مهمة",
            //       moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Change task progress",
            //        IsEnable=false,
            //       NameArabic = "تغيير تططور المهمه",
            //    moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Return task",
            //        IsEnable=false,
            //       NameArabic = "ارجاع المهمة",
            //   moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Complete task",
            //        IsEnable=false,
            //       NameArabic = "اتمام المهمة",
            //moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },

            //   new NotificationTypeTemplate{
            //       NameEnglish = "Delayed task",
            //        IsEnable=false,
            //       NameArabic = "تاخير المهمه",
            //    moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Pending review",
            //        IsEnable=false,
            //       NameArabic = "مراجعه موقفه",
            //   moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Request end date change",
            //       IsEnable=false,
            //       NameArabic = "طلب تغيير تاريخ الانهاء",
            //      moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Approve or reject request end date change",
            //     IsEnable=false,
            //       NameArabic = "قبول او رفض تغيير تاريخ الانهاء",
            //      moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   },
            //      new NotificationTypeTemplate{
            //       NameEnglish = "Send support request",
            //       IsEnable=false,
            //       NameArabic = "ارسال طلب مساعده",
            //       moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Edit task",
            //       IsEnable=false,
            //       NameArabic = "تعديل المهمه",
            //    moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Reject task",
            //       IsEnable=false,
            //       NameArabic = "رفض المهمه",
            //        moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   },
            //    new NotificationTypeTemplate{
            //       NameEnglish = "Change task priority",
            //       IsEnable=false,
            //       NameArabic = "تغيير اولويه المهمه",
            //                          moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Escalate task",
            //         IsEnable=false,
            //       NameArabic = "جدوله المهمه",
            //    moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   }, new NotificationTypeTemplate{
            //       NameEnglish = "Delete task ",
            //         IsEnable=false,
            //       NameArabic = "حذف المهمه",
            //    moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Mention user in comment",
            //        IsEnable = false,
            //       NameArabic = "تذكير مستخدم فى التعليقات",
            //       moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   }, new NotificationTypeTemplate{
            //       NameEnglish = "Add Delegation",
            //       IsEnable = false, NameArabic = "اضافه تفويض",
            //       moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Delete delegation",
            //       IsEnable=false,
            //       NameArabic = "حذف تفويض",
            //       moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   }, new NotificationTypeTemplate{
            //       NameEnglish = "Edit delegation",
            //         IsEnable=false,
            //       NameArabic = "تعديل تفويض",
            //      moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Add escalation",
            //       IsEnable=false,
            //       NameArabic = "اضافه جدوله",
            //        moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   }, new NotificationTypeTemplate{
            //       NameEnglish = "Delete escalation",
            //         IsEnable=false,
            //       NameArabic = "حذف جدوله",
            //        moduleType= Dto.Enums.ModuleType.SystemNotification,


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Edit escalation",
            //            IsEnable=false,
            //       NameArabic = "تعديل جدوله",
            //      moduleType= Dto.Enums.ModuleType.SystemNotification,

            //   },


            //   //mail 


            //    new NotificationTypeTemplate{
            //       NameEnglish = "Task creation",
            //       NameArabic = "إنشاء مهمة",
            //       IsEnable=false,
            //       moduleType= Dto.Enums.ModuleType.Email,
            //       SubjectArabic="",
            //       SubjectEnglish="",
            //       TemplateArabic="",
            //       TemplateEnglish="",

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Change task progress",
            //        IsEnable=false,
            //       NameArabic = "تغيير تطور المهمه",
            //    moduleType= Dto.Enums.ModuleType.Email,
            //    SubjectArabic="[TaskName] – تم تحديث تقدم المهمة",
            //    SubjectEnglish="Task progress updated – [TaskName]",
            //    TemplateEnglish="<p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Dear [RecipientName],<br><br>The task [TaskName] has been updated.<br>New progress: [Progress]%</p><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,<br></p>",
            //    TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"></p><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-weight: 400;\">.تم تحديث مهمة [<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">TaskName</span>]</span><br><span style=\"display: inline !important;\">. %نسبة التقدم الجديدة&nbsp;</span>[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">Progress</span>]</div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">,مع التحية</div>",


            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Return task",
            //        IsEnable=false,
            //       NameArabic = "ارجاع المهمة",
            //   moduleType= Dto.Enums.ModuleType.Email,
            //    SubjectArabic="[TaskName] – تم إعادة المهمة",
            //    SubjectEnglish="Task reopened – [TaskName]",
            //TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\">RecipientName</b>]<br></span><br style=\"font-weight: 400;\"></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span>. تمت إعادة مهمة&nbsp;<span style=\"display: inline !important;\">[<b class=\"lastNode\" style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\">TaskName</b>] لإجراء تحديثات إضافية</span><br></span><div>.يرجى مراجعة التفاصيل والمتابعة وفقًا لذلك</div><br></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\">,مع التحية</b></div>",
            //TemplateEnglish="<p style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\">Dear [RecipientName],<br><br>The task [TaskName] has been reopened for further updates.<br><span style=\"display: inline !important;\">Please review the details and proceed accordingly.</span></b></p><p style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\">Regards,</b></p>",

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Complete task",
            //        IsEnable=false,
            //       NameArabic = "اتمام المهمة",
            //moduleType= Dto.Enums.ModuleType.Email,
            //SubjectArabic="تم إكمال المهمة  [TaskName]",
            //SubjectEnglish="Task completed [TaskName]",
            //TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\">RecipientName</b></b>]<br></span><br style=\"font-weight: 400;\"></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><span>.تم وضع علامة \"مكتملة\" على مهمة&nbsp;<span style=\"display: inline !important;\">[<b class=\"lastNode\" style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\">TaskName</span></b></b>]</span><br></span><div><br></div></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b>,مع التحية</b></b></div>",
            //TemplateEnglish="<div style=\"font-weight: 400; margin-top: 14px; margin-bottom: 14px;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b>Dear [RecipientName],<br><br><span style=\"display: inline !important;\">The task [TaskName] has been marked as completed.</span></b></b></div><p style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b>Regards,</b></b></p>",


            //   },

            //   new NotificationTypeTemplate{
            //       NameEnglish = "Delayed task",
            //        IsEnable=false,
            //       NameArabic = "تاخير المهمه",
            //    moduleType= Dto.Enums.ModuleType.Email,
            //    SubjectArabic="[TaskName] – مهمة متأخرة",
            //    SubjectEnglish="ask delayed – [TaskName]",
            //    TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\">RecipientName</b></b></b>]<br></span><br style=\"font-weight: 400;\"></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><span>.تم وضع علامة \"متأخرة\" على مهمة&nbsp;<span style=\"display: inline !important;\">[<b class=\"lastNode\" style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\">TaskName</span></b></b></b>]</span><br></span><div><br></div></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b>,مع التحية</b></b></b></div>",
            //    TemplateEnglish="<div style=\"font-weight: 400; margin-top: 14px; margin-bottom: 14px;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b>Dear [RecipientName],<br><span style=\"display: inline !important;\">The task [TaskName] has been marked as delayed.</span></b></b></b></div><p style=\"font-weight: 400; margin-top: 14px; margin-bottom: 14px;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><span style=\"display: inline !important;\"></span></b></b></b><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b>Regards,</b></b></b></p>",

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Pending review",
            //        IsEnable=false,
            //       NameArabic = "مراجعه موقفه",
            //   moduleType= Dto.Enums.ModuleType.Email,
            //   SubjectArabic="",
            //   SubjectEnglish="",
            //   TemplateArabic="",
            //   TemplateEnglish=""

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Request end date change",
            //       IsEnable=false,
            //       NameArabic = "طلب تغيير تاريخ الانهاء",
            //      moduleType= Dto.Enums.ModuleType.Email,
            //      SubjectArabic="[TaskName] – طلب تغيير تاريخ الانتهاء",
            //      SubjectEnglish="End date change requested – [TaskName]",
            //      TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\">RecipientName</b></b></b></b>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم تقديم طلب لتغيير تاريخ الانتهاء للمهمة&nbsp;<span style=\"display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\"><span style=\"font-family: Indivisible;\">TaskName</span></span></b></b></b></b>]<br></span><span>.تاريخ الانتهاء المطلوب&nbsp;<span style=\"display: inline !important;\">[<b class=\"lastNode\" style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\"><span style=\"font-family: Indivisible;\">RequestedEndDate</span></span></b></b></b></b>]</span></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //      TemplateEnglish="<div style=\"font-weight: 400; margin-top: 14px; margin-bottom: 14px;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>Dear [RecipientName],<br><span style=\"display: inline !important;\"><br><span>A request to change the end date for [TaskName] has been submitted.<br></span><span>Requested end date: [RequestedEndDate]</span></span></b></b></b></b></div><p style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>Regards,</b></b></b></b></p>",

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Approve or reject request end date change",
            //     IsEnable=false,
            //       NameArabic = "قبول او رفض تغيير تاريخ الانهاء",
            //      moduleType= Dto.Enums.ModuleType.Email,
            //      SubjectArabic="[TaskName] – قرار بشأن تغيير تاريخ الانتهاء",
            //      SubjectEnglish="End date change decision – [TaskName]",
            //        TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\">RecipientName</b></b></b></b>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم [القرار (قبول, رفض)] طلب تغيير تاريخ الانتهاء للمهمة&nbsp;<span style=\"display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\"><span style=\"font-family: Indivisible;\"><span style=\"font-family: Indivisible;\">TaskName</span></span></span></b></b></b></b>]<br></span><span>.تاريخ الانتهاء الجديد&nbsp;<span style=\"display: inline !important;\">[<b class=\"lastNode\" style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\"><span style=\"font-family: Indivisible;\"><span style=\"font-family: Indivisible;\">NewEndDate</span></span></span></b></b></b></b>]</span></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //        TemplateEnglish="<div style=\"font-weight: 400; margin-top: 14px; margin-bottom: 14px;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>Dear [RecipientName],<br><span style=\"display: inline !important;\"><span><br><span>Your request to change the end date for [TaskName] has been [Decision (Approved, Rejected)].<br></span><span>New end date: [NewEndDate]</span><br></span></span></b></b></b></b></div><p style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>Regards,</b></b></b></b></p>",

            //   },
            //      new NotificationTypeTemplate{
            //       NameEnglish = "Reject the end date change request",
            //       IsEnable=false,
            //       NameArabic = "رفض طلب تغيير تاريخ الانتهاء",
            //       moduleType= Dto.Enums.ModuleType.Email,
            //        SubjectArabic="[TaskName] – قرار بشأن تغيير تاريخ الانتهاء",
            //   SubjectEnglish=" End date change decision – [TaskName]",
            //   TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\">RecipientName</b></b></b></b>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\"><span style=\"font-family: Indivisible;\"><span style=\"font-family: Indivisible;\">Decision (Approved, Rejected)]</span></span></span></b></b></b></b>] طلب تغيير تاريخ الانتهاء للمهمة&nbsp;<span style=\"display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\"><span style=\"font-family: Indivisible;\"><span style=\"font-family: Indivisible;\">TaskName</span></span></span></b></b></b></b>]<br></span><span>.تاريخ الانتهاء الجديد&nbsp;<span style=\"display: inline !important;\">[<b class=\"lastNode\" style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><b style=\"font-weight: bold; font-family: Indivisible;\"><span style=\"font-family: Indivisible; display: inline !important;\"><span style=\"font-family: Indivisible;\"><span style=\"font-family: Indivisible;\">NewEndDate</span></span></span></b></b></b></b>]</span></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //   TemplateEnglish="<div style=\"font-weight: 400; margin-top: 14px; margin-bottom: 14px;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>Dear [RecipientName],<br><span style=\"display: inline !important;\"><span><br><span>Your request to change the end date for [TaskName] has been [Decision (Approved, Rejected)].<br></span><span>New end date: [NewEndDate]</span><br></span></span></b></b></b></b></div><p style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>Regards,</b></b></b></b></p>"

            //   },
            //       new NotificationTypeTemplate{
            //       NameEnglish = "Weekly report",
            //       IsEnable=false,
            //       NameArabic = "تقرير اسبوعى",
            //       moduleType= Dto.Enums.ModuleType.Email,
            //      SubjectArabic="التقرير الأسبوعي للمهام",
            //   SubjectEnglish=" Weekly task report",
            //   TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b><span>.يرجى الاطلاع على ملخص مهامك الأسبوعي داخل النظام<span style=\"display: inline !important;\"><br></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //   TemplateEnglish="<p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Dear [RecipientName],<br><br>Please find your weekly task summary in the system.</p><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>"

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Edit task",
            //       IsEnable=false,
            //       NameArabic = "تعديل المهمه",
            //    moduleType= Dto.Enums.ModuleType.Email,
            //    SubjectArabic="[TaskName] – تم تحديث المهمة",
            //    SubjectEnglish="Task updated – [TaskName]",
            //TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم تحديث المهمة&nbsp;<span style=\"display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">TaskName</span>]<br></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //TemplateEnglish="<p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Dear [RecipientName],<br><br>The task [TaskName] has been updated.</p><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>",

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Reject task",
            //       IsEnable=false,
            //       NameArabic = "رفض المهمه",
            //        moduleType= Dto.Enums.ModuleType.Email,
            //        SubjectArabic=" [TaskName] – تم رفض المهمة",
            //        SubjectEnglish="ask rejected – [TaskName]",
            //        TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">.تم رفض مهمة [<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">TaskName</span>]<br>.يرجى مراجعة التفاصيل والمتابعة وفقًا لذلك</b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><br>,مع التحية</b></div>",
            //        TemplateEnglish="<p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-weight: 400;\">Dear [RecipientName],</span><br><br></p><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"font-weight: 400;\">The ta</span>sk [TaskName] h<span style=\"font-weight: 400;\">as been rejected.</span><br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Please review the details and proceed accordingly.</div><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>",


            //   },
            //    new NotificationTypeTemplate{
            //       NameEnglish = "Change task priority",
            //       IsEnable=false,
            //       NameArabic = "تغيير اولويه المهمه",
            //       moduleType= Dto.Enums.ModuleType.Email,
            //       SubjectArabic="[TaskName] – تم تحديث أولوية المهمة",
            //       SubjectEnglish="Task Priority Updated – [TaskName]",
            //       TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم<span>&nbsp;</span>تحديث أولوية مهمة&nbsp;<span style=\"display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">TaskName</span>]<br></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //       TemplateEnglish="<p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Dear [RecipientName],<br><br>The priority of task [TaskName] has been updated.</p><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Escalate task",
            //         IsEnable=false,
            //       NameArabic = "جدوله المهمه",
            //    moduleType= Dto.Enums.ModuleType.Email,
            //    SubjectArabic=" [TaskName] – تم تصعيد المهمة",
            //    SubjectEnglish="Task Escalated – [TaskName]",
            //    TemplateEnglish="<p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Dear [RecipientName],<br><br>The task [Task Name] has been escalated due to unresolved issues.<br>Escalation level: [EscalationLevel]</p><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>",
            //    TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم تصعيد مهمة [<span style=\"display: inline !important;\">اسم المهمة</span>] بسبب وجود مشكلات لم يتم حلها<br><span>.مستوى التصعيد:&nbsp;<span style=\"display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">EscalationLevel</span>]</span></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",


            //   }, new NotificationTypeTemplate{
            //       NameEnglish = "Delete task ",
            //         IsEnable=false,
            //       NameArabic = "حذف المهمه",
            //    moduleType= Dto.Enums.ModuleType.Email,
            //     SubjectArabic="[TaskName] – تم حذف المهمة",
            //   SubjectEnglish="Task deleted– [TaskName]",
            //   TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم حذف مهمة&nbsp;<span style=\"display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">TaskName</span>]<br></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //   TemplateEnglish="<p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Dear [RecipientName],<br><br>The task [TaskName] has been deleted.</p><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>"

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Mention user in comment",
            //        IsEnable = false,
            //       NameArabic = "تذكير مستخدم فى التعليقات",
            //       moduleType= Dto.Enums.ModuleType.Email,
            //        SubjectArabic="[TaskName] – تم ذكرك في تعليق",
            //   SubjectEnglish="You were mentioned in a comment – [TaskName]",
            //   TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تمت الإشارة إليك في تعليق على المهمة&nbsp;<span style=\"display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">TaskName</span>]<br></span></span><div><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //   TemplateEnglish="<p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Dear [RecipientName],<br><br>You were mentioned in a comment on task [TaskName].</p><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>"

            //   }, new NotificationTypeTemplate{
            //       NameEnglish = "Add delegation",
            //       IsEnable = false, NameArabic = "اضافه تفويض",
            //       moduleType= Dto.Enums.ModuleType.Email,
            //        SubjectArabic="تم إضافة تفويض",
            //   SubjectEnglish="Delegation added",
            //   TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<b style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\">RecipientName</b>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تمت إضافة تفويض جديد إلى حسابك<span style=\"display: inline !important;\"><br></span></span><div>.المفوّض من: [<b class=\"lastNode\" style=\"font-weight: bold; font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; color: rgb(0, 0, 0); background-color: rgb(255, 255, 255);\">Delegatedfromname</b>]<br><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //   TemplateEnglish="<p style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\">Dear [RecipientName],<br><br>A new delegation has been added to your account.<br>Delagated from: [Delegatedfromname]</b></p><p style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\">Regards,</b></p>"

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Delete delegation",
            //       IsEnable=false,
            //       NameArabic = "حذف تفويض",
            //       moduleType= Dto.Enums.ModuleType.Email,
            //        SubjectArabic="تم حذف التفويض",
            //        SubjectEnglish="Delegation removed",
            //        TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم حذف أحد التفويضات من حسابك<span style=\"display: inline !important;\"><br></span></span><div>.المفوّض من: [<span style=\"color: rgb(0, 0, 0); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">Delegatedfromname</span>]<br><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //        TemplateEnglish="<div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); margin-top: 14px; margin-bottom: 14px;\">Dear [RecipientName],<br><br>A delegation has been removed from your account.<br><span><p>Delagated from: [Delegatedfromname]</p></span></div><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>"

            //   }, new NotificationTypeTemplate{
            //       NameEnglish = "Edit delegation",
            //         IsEnable=false,
            //       NameArabic = "تعديل تفويض",
            //      moduleType= Dto.Enums.ModuleType.Email,
            //       SubjectArabic="تم تعديل التفويض",
            //   SubjectEnglish="Delegation updated",
            //   TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span><br style=\"font-weight: 400;\"></b></b></b></b></b></b></b></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><span>.تم تعديل أحد التفويضات المرتبطة بحسابك<span style=\"display: inline !important;\"><br></span></span><div>.المفوّض من: [<span style=\"color: rgb(33, 37, 41); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">Delegatedfromname</span>]<br><br></div></b></b></b></b></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //   TemplateEnglish="<p></p><div style=\"font-weight: 400; margin-top: 14px; margin-bottom: 14px;\">Dear [RecipientName],<br><br>A delegation linked to your account has been updated.<br><span><p>Delagated from: [Delegatedfromname]</p></span></div><p></p>"

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Add escalation",
            //       IsEnable=false,
            //       NameArabic = "اضافه جدوله",
            //        moduleType= Dto.Enums.ModuleType.Email,
            //         SubjectArabic="تم إضافة تصعيد",
            //   SubjectEnglish="Escalation added",
            //   TemplateArabic="<p><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><b><b><b><b><b><b><b><b><b><b><b><span style=\"font-weight: 400; display: inline !important;\">عزيزي/عزيزتي&nbsp;</span><span style=\"font-weight: 400; display: inline !important;\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipientName</span>]<br></span></b></b></b></b></b></b></b></b></b></b></b></b></b></b></b></p><div style=\"font-weight: 400;\"><div><div><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><b><b><b><br>توجد مشكلة داخل [<span style=\"color: rgb(0, 0, 0); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">TaskName</span>] وتتطلب التصعيد الفوري. على الرغم من جهودنا لحل المشكلة على مستوى الفريق، إلا أنها لا تزال دون حل وتؤثر على التقدم.&nbsp;&nbsp;<br></b></b></b></b></div><div><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>مستوى التصعيد: [<span style=\"color: rgb(0, 0, 0); font-family: Indivisible; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\" class=\"lastNode\">EscalationLevel</span>]</b></b></b></b></div><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b><br></b></b></b></b></div></div><div style=\"font-weight: 400;\"><b style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\" class=\"lastNode\"><b><b><b>,مع التحية</b></b></b></b></div>",
            //   TemplateEnglish="<div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); margin-top: 14px; margin-bottom: 14px;\"><p>Dear [RecipientName],<br><br></p><div>There is an issue within [TaskName] that requires immediate escalation. Despite our efforts to resolve the matter at the team level, it remains unresolved and is impacting progress.<br></div><div>Escalation level: [EscalationLevel]</div><p><br>A new escalation has been added for task [TaskName].</p></div><p style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Regards,</p>"

            //   }, new NotificationTypeTemplate{
            //       NameEnglish = "Delete escalation",
            //         IsEnable=false,
            //       NameArabic = "حذف جدوله",
            //        moduleType= Dto.Enums.ModuleType.Email,
            //         SubjectArabic="حذف الجدوله",
            //   SubjectEnglish="Delete Escalation",
            //   TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"display: inline !important;\">,عزيزي/عزيزتي&nbsp;</span>[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipienName</span>]&nbsp;&nbsp;<br></span></p><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">توجد مشكلة داخل [<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">TaskName</span>] وتتطلب التصعيد الفوري. على الرغم من جهودنا لحل المشكلة على مستوى الفريق، إلا أنها لا تزال دون حل وتؤثر على التقدم. &nbsp;<br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">مستوى التصعيد: [@Escalation_Level]<br><br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">مع الشكر، &nbsp;<br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">SenderName</span>]</div>",
            //   TemplateEnglish="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">Dear [RecipienName],</span><br style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"></p><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">There is an issue within [TaskName] that requires immediate escalation. Despite our efforts to resolve the matter at the team level, it remains unresolved and is impacting progress.<br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Escalation level: [@Escalation_Level]</div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><br>Regards, &nbsp;<br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">[SenderName]</div>"

            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Edit escalation",
            //            IsEnable=false,
            //       NameArabic = "تعديل جدوله",
            //      moduleType= Dto.Enums.ModuleType.Email,
            //      SubjectArabic="حذف الجدوله",
            //      SubjectEnglish="Delete Escalation",
            //      TemplateArabic="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><span style=\"display: inline !important;\">,عزيزي/عزيزتي&nbsp;</span>[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">RecipienName</span>]&nbsp;&nbsp;<br></span></p><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">توجد مشكلة داخل [<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">TaskName</span>] وتتطلب التصعيد الفوري. على الرغم من جهودنا لحل المشكلة على مستوى الفريق، إلا أنها لا تزال دون حل وتؤثر على التقدم. &nbsp;<br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">مستوى التصعيد: [@Escalation_Level]<br><br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">مع الشكر، &nbsp;<br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">[<span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">SenderName</span>]</div>",
            //      TemplateEnglish="<p style=\"text-align: start;\"><span style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255); float: none; display: inline !important;\">Dear [RecipienName],</span><br style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"></p><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">There is an issue within [TaskName] that requires immediate escalation. Despite our efforts to resolve the matter at the team level, it remains unresolved and is impacting progress.<br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">Escalation level: [@Escalation_Level]</div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\"><br>Regards, &nbsp;<br></div><div style=\"color: rgb(0, 0, 0); font-family: &quot;Segoe UI VSS (Regular)&quot;, &quot;Segoe UI&quot;, -apple-system, BlinkMacSystemFont, Roboto, &quot;Helvetica Neue&quot;, Helvetica, Ubuntu, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-weight: 400; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; background-color: rgb(255, 255, 255);\">[SenderName]</div>",
            //   },

            //   //History
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Task creation",
            //       IsEnable=true,
            //       NameArabic = "إنشاء مهمة",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المنشئ] بإنشاء المهمة",
            //       TemplateEnglish="[User Name] created the task",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Change task progress",
            //        IsEnable=true,
            //       NameArabic = "تغيير تقدم المهمة",
            //    moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="[قام [اسم المستخدم] بتحديث تقدم المهمة إلى [نسبة التقدم الجديدة",
            //       TemplateEnglish="[User Name] updated the task progress to [New Progress %]",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Return task",
            //        IsEnable=true,
            //       NameArabic = "ارجاع المهمة",
            //   moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بإرجاع المهمة, بسبب: [سبب الإرجاع]",
            //       TemplateEnglish="[User Name] returned the task with the reason: [Return Reason]",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Complete task",
            //        IsEnable=true,
            //       NameArabic = "إكمال المهمة",
            //moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بإكمال المهمة",
            //       TemplateEnglish="[User Name] marked the task as completed",
            //   },

            //   new NotificationTypeTemplate{
            //       NameEnglish = "Delayed task",
            //        IsEnable=true,
            //       NameArabic = "مهمة متأخرة",
            //    moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="لقد تأخرت المهمة",
            //       TemplateEnglish="The task due date is overdue",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Ready for review task",
            //        IsEnable=true,
            //       NameArabic = "مهمة جاهزة للمراجعة",
            //   moduleType= Dto.Enums.ModuleType.History,
            //   TemplateArabic="قام [اسم المستخدم] بنقل المهمة على أنها جاهزة للمراجعة",
            //   TemplateEnglish="[User Name] marked the task as ready for review",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Add attachment",
            //        IsEnable=true,
            //       NameArabic = "إضافة ملف",
            //   moduleType= Dto.Enums.ModuleType.History,
            //   TemplateArabic="قام [اسم المستخدم] بإضافة مرفق [اسم المرفق]",
            //   TemplateEnglish="[User Name] added an attachment [Attachment Name]",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Delete attachment",
            //        IsEnable=true,
            //       NameArabic = "حذف ملف",
            //   moduleType= Dto.Enums.ModuleType.History,
            //   TemplateArabic="قام [اسم المستخدم] بحذف مرفق [اسم المرفق]",
            //   TemplateEnglish="[User Name] deleted an attachment [Attachment Name]",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Request end date change",
            //       IsEnable=true,
            //       NameArabic = "طلب تغيير تاريخ الانتهاء",
            //      moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="طلب [اسم المستخدم] تغيير تاريخ انتهاء المهمة من [التاريخ القديم] إلى [التاريخ الجديد]. مع السبب: [السبب]",
            //       TemplateEnglish="[User Name] requested to change the task end date from [Old Date] to [New Date], with reason: [Reason]",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Approve or reject the request end date change ",
            //     IsEnable=true,
            //       NameArabic = "الموافقة على طلب تغيير تاريخ الانتهاء أو رفضه",
            //      moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="[اسم المستخدم] [حالة الإجراء (تمت الموافقه أو تم رفضه)] تاريخ انتهاء المهمة من [التاريخ القديم] إلى [التاريخ الجديد]",
            //       TemplateEnglish="[User Name] [Action Status (Approved or Rejected)] the task end date from [Old Date] to [New Date]",
            //   },
            //      new NotificationTypeTemplate{
            //       NameEnglish = "Reject the end date change request",
            //       IsEnable=true,
            //       NameArabic = "رفض طلب تغيير تاريخ الانتهاء",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="[اسم المستخدم] [حالة الإجراء (تمت الموافقه أو تم رفضه)] تاريخ انتهاء المهمة من [التاريخ القديم] إلى [التاريخ الجديد]",
            //       TemplateEnglish="[User Name] [Action Status (Approved or Rejected)] the task end date from [Old Date] to [New Date]",
            //   },
            //       new NotificationTypeTemplate{
            //       NameEnglish = "Weekly report",
            //       IsEnable=true,
            //       NameArabic = "تقرير اسبوعى",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="أرسل [اسم المستخدم] طلب دعم إلى [الاسم المدعوم]",
            //       TemplateEnglish="[User Name] sent a support request to [Supported Name]",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Edit task",
            //       IsEnable=true,
            //       NameArabic = "تعديل المهمة",
            //    moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بتعديل المهمة",
            //       TemplateEnglish="[User Name] edited the task",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Reject task",
            //       IsEnable=true,
            //       NameArabic = "رفض المهمه",
            //        moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] برفض المهمة، مع السبب: [سبب الرفض]",
            //       TemplateEnglish="[User Name] rejected the task, with reason: [Rejection Reason]",
            //   },
            //    new NotificationTypeTemplate{
            //       NameEnglish = "Change task priority",
            //       IsEnable=true,
            //       NameArabic = "تغيير اولويه المهمه",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بتغيير الأولوية من [الأولوية القديمة] إلى [الأولوية الجديدة]",
            //       TemplateEnglish="[User Name] changed the priority from [Old Priority] to [New Priority]",
            //    },
            //     new NotificationTypeTemplate{
            //       NameEnglish = "Edit task description",
            //       IsEnable=true,
            //       NameArabic = "تعديل وصف المهمة",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بتحديث وصف المهمة",
            //       TemplateEnglish="[User Name] updated the task description",
            //     },
            //       new NotificationTypeTemplate{
            //       NameEnglish = "Add a task checklist",
            //       IsEnable=true,
            //       NameArabic = "إضافة قائمة تحقق للمهام",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بإضافة قائمة تحقق [عنصر قائمة تحقق]",
            //       TemplateEnglish="[User Name] added a checklist [Checklist Item]",
            //       },
            //       new NotificationTypeTemplate{
            //       NameEnglish = "Delete the task checklist",
            //       IsEnable=true,
            //       NameArabic = "حذف قائمة تحقق المهام",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بإزالة قائمة التحقق [عنصر قائمة مراجعة]",
            //       TemplateEnglish="[User Name] removed a checklist [Checklist Item]",
            //       },
            //        new NotificationTypeTemplate{
            //       NameEnglish = "Update the task checklist",
            //       IsEnable=true,
            //       NameArabic = "تحديث قائمة تحقق المهام",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بتعديل عنصر قائمة مراجعة [عنصر قائمة مراجعة]",
            //       TemplateEnglish="[User Name] Updated a checklist item [Checklist Item]",
            //        },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Escalate task",
            //         IsEnable=true,
            //       NameArabic = "تصعيد مهمة",
            //    moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="تم تصعيد المهمة إلى [استُبعدت إلى المستخدم]، مع السبب: [سبب التصعيد]",
            //       TemplateEnglish="The task is escalated to [Exclated To User], with reason: [Escalation Reason]",
            //   },
            //                  new NotificationTypeTemplate{
            //       NameEnglish = "Add comment",
            //         IsEnable=true,
            //       NameArabic = "إضافة تعليق",
            //    moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بإضافة تعليق",
            //       TemplateEnglish="[User Name] added a comment",
            //   },
            //     new NotificationTypeTemplate{
            //       NameEnglish = "Delete comment",
            //         IsEnable=true,
            //       NameArabic = "حذف تعليق",
            //    moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بحذف تعليق",
            //       TemplateEnglish="[User Name] deleted a comment",
            //   },
            //    new NotificationTypeTemplate{
            //       NameEnglish = "Delete task ",
            //         IsEnable=true,
            //       NameArabic = "حذف مهمة",
            //    moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بحذف المهمة",
            //       TemplateEnglish="[User Name] deleted the task",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Restore the deleted task",
            //        IsEnable = true,
            //       NameArabic = "استعادة المهمة المحذوفة",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بإستعاد المهمة المحذوفة",
            //       TemplateEnglish="[User Name] restored the deleted task",
            //   },
            //                  new NotificationTypeTemplate{
            //       NameEnglish = "Mention user in comment",
            //        IsEnable = true,
            //       NameArabic = "ذكر اسم المستخدم في التعليق",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="قام [اسم المستخدم] بذكر [اسم المستخدم المذكور]في التعليق",
            //       TemplateEnglish="[User Name] mentioned [Mentioned User Name] in the comment.",
            //   },
            //    new NotificationTypeTemplate{
            //       NameEnglish = "Add delegation",
            //       IsEnable = true, NameArabic = "اضافه تفويض",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="",
            //       TemplateEnglish="",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Delete delegation",
            //       IsEnable=true,
            //       NameArabic = "حذف تفويض",
            //       moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="",
            //       TemplateEnglish="",
            //   },
            //    new NotificationTypeTemplate{
            //       NameEnglish = "Edit delegation",
            //         IsEnable=true,
            //       NameArabic = "تعديل تفويض",
            //      moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="",
            //       TemplateEnglish="",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Add escalation",
            //       IsEnable=true,
            //       NameArabic = "اضافه جدوله",
            //        moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="",
            //       TemplateEnglish="",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Delete escalation",
            //       IsEnable=true,
            //       NameArabic = "حذف جدوله",
            //        moduleType= Dto.Enums.ModuleType.History,
            //        TemplateArabic="@Action_owner قام بحذف التصعيد الذي تم إرساله لهذه المهمة @Task_Name",
            //       TemplateEnglish="@Action_owner deleted the raised escalation that was sent for task @Task_Name\r\n",
            //   },
            //   new NotificationTypeTemplate{
            //       NameEnglish = "Edit escalation",
            //       IsEnable=true,
            //       NameArabic = "تعديل جدوله",
            //        moduleType= Dto.Enums.ModuleType.History,
            //       TemplateArabic="",
            //       TemplateEnglish="",
            //   }

            //};

            var existingNames = context.NotificationTypeTemplate
                .Select(p => p.NameEnglish)
                .ToHashSet();
            var existingtypes = context.NotificationTypeTemplate
               .Select(p => p.moduleType)
               .ToHashSet();

            foreach (var notificationTypeTemplate in NotificationTypeTemplatesToSeed)
            {
                if (context.NotificationTypeTemplate.Any(k => k.NameEnglish == notificationTypeTemplate.NameEnglish && k.moduleType == notificationTypeTemplate.moduleType))
                { }
                else
                {
                    context.NotificationTypeTemplate.Add(notificationTypeTemplate);
                }
            }


            context.SaveChanges();
        }
    }
}
