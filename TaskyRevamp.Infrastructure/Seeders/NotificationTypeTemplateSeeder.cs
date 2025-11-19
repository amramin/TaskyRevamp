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
                new NotificationTypeTemplate{
                   NameEnglish = "Task creation",
                   NameArabic = "إنشاء مهمة",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Change task progress",
                   NameArabic = "تغيير تططور المهمه",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Return task",
                   NameArabic = "ارجاع المهمة",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Complete task",
                   NameArabic = "اتمام المهمة",

               },

               new NotificationTypeTemplate{
                   NameEnglish = "Delayed task",
                   NameArabic = "تاخير المهمه",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Pending review",
                   NameArabic = "مراجعه موقفه",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Request end date change",
                   NameArabic = "طلب تغيير تاريخ الانهاء",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Approve or reject request end date change",
                   NameArabic = "قبول او رفض تغيير تاريخ الانهاء",

               },
                  new NotificationTypeTemplate{
                   NameEnglish = "Send support request",
                   NameArabic = "ارسال طلب مساعده",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Edit task",
                   NameArabic = "تعديل المهمه",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Reject task",
                   NameArabic = "رفض المهمه",

               },
                new NotificationTypeTemplate{
                   NameEnglish = "Change task priority",
                   NameArabic = "تغيير اولويه المهمه",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Escalate task",
                   NameArabic = "جدوله المهمه",

               }, new NotificationTypeTemplate{
                   NameEnglish = "Delete task ",
                   NameArabic = "حذف المهمه",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Mention user in comment",
                   NameArabic = "تذكير مستخدم فى التعليقات",

               }, new NotificationTypeTemplate{
                   NameEnglish = "Add Delegation",
                   NameArabic = "اضافه تفويض",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Delete delegation",
                   NameArabic = "حذف تفويض",

               }, new NotificationTypeTemplate{
                   NameEnglish = "Edit delegation",
                   NameArabic = "تعديل تفويض",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Add Escalation",
                   NameArabic = "اضافه جدوله",

               }, new NotificationTypeTemplate{
                   NameEnglish = "Delete Escalation",
                   NameArabic = "حذف جدوله",

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Edit Escalation",
                   NameArabic = "تعديل جدوله",

               }
            };

            var existingNames = context.NotificationTypeTemplate
                .Select(p => p.NameEnglish)
                .ToHashSet();

            foreach (var notificationTypeTemplate in NotificationTypeTemplatesToSeed)
            {
                if (!existingNames.Contains(notificationTypeTemplate.NameEnglish))
                {
                    context.NotificationTypeTemplate.Add(notificationTypeTemplate);
                }
            }


            context.SaveChanges();
        }
    }
}
