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
                   IsEnable=true,
                   NameArabic = "إنشاء مهمة",
                   moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Change task progress",
                    IsEnable=true,
                   NameArabic = "تغيير تططور المهمه",
                moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Return task",
                    IsEnable=true,
                   NameArabic = "ارجاع المهمة",
               moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Complete task",
                    IsEnable=true,
                   NameArabic = "اتمام المهمة",
            moduleType= Dto.Enums.ModuleType.SystemNotification,


               },

               new NotificationTypeTemplate{
                   NameEnglish = "Delayed task",
                    IsEnable=true,
                   NameArabic = "تاخير المهمه",
                moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Pending review",
                    IsEnable=true,
                   NameArabic = "مراجعه موقفه",
               moduleType= Dto.Enums.ModuleType.SystemNotification,

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Request end date change",
                   IsEnable=true,
                   NameArabic = "طلب تغيير تاريخ الانهاء",
                  moduleType= Dto.Enums.ModuleType.SystemNotification,

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Approve or reject request end date change",
                 IsEnable=true,
                   NameArabic = "قبول او رفض تغيير تاريخ الانهاء",
                  moduleType= Dto.Enums.ModuleType.SystemNotification,

               },
                  new NotificationTypeTemplate{
                   NameEnglish = "Send support request",
                   IsEnable=true,
                   NameArabic = "ارسال طلب مساعده",
                   moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Edit task",
                   IsEnable=true,
                   NameArabic = "تعديل المهمه",
                moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Reject task",
                   IsEnable=true,
                   NameArabic = "رفض المهمه",
                    moduleType= Dto.Enums.ModuleType.SystemNotification,

               },
                new NotificationTypeTemplate{
                   NameEnglish = "Change task priority",
                   IsEnable=true,
                   NameArabic = "تغيير اولويه المهمه",
                                      moduleType= Dto.Enums.ModuleType.SystemNotification,

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Escalate task",
                     IsEnable=true,
                   NameArabic = "جدوله المهمه",
                moduleType= Dto.Enums.ModuleType.SystemNotification,


               }, new NotificationTypeTemplate{
                   NameEnglish = "Delete task ",
                     IsEnable=true,
                   NameArabic = "حذف المهمه",
                moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Mention user in comment",
                    IsEnable = true,
                   NameArabic = "تذكير مستخدم فى التعليقات",
                   moduleType= Dto.Enums.ModuleType.SystemNotification,

               }, new NotificationTypeTemplate{
                   NameEnglish = "Add Delegation",
                   IsEnable = true, NameArabic = "اضافه تفويض",
                   moduleType= Dto.Enums.ModuleType.SystemNotification,

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Delete delegation",
                   IsEnable=true,
                   NameArabic = "حذف تفويض",
                   moduleType= Dto.Enums.ModuleType.SystemNotification,

               }, new NotificationTypeTemplate{
                   NameEnglish = "Edit delegation",
                     IsEnable=true,
                   NameArabic = "تعديل تفويض",
                  moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Add escalation",
                   IsEnable=true,
                   NameArabic = "اضافه جدوله",
                    moduleType= Dto.Enums.ModuleType.SystemNotification,

               }, new NotificationTypeTemplate{
                   NameEnglish = "Delete escalation",
                     IsEnable=true,
                   NameArabic = "حذف جدوله",
                    moduleType= Dto.Enums.ModuleType.SystemNotification,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Edit escalation",
                        IsEnable=true,
                   NameArabic = "تعديل جدوله",
                  moduleType= Dto.Enums.ModuleType.SystemNotification,

               },


               //mail 

            
                new NotificationTypeTemplate{
                   NameEnglish = "Task creation",
                   IsEnable=true,
                   NameArabic = "إنشاء مهمة",
                   moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Change task progress",
                    IsEnable=true,
                   NameArabic = "تغيير تططور المهمه",
                moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Return task",
                    IsEnable=true,
                   NameArabic = "ارجاع المهمة",
               moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Complete task",
                    IsEnable=true,
                   NameArabic = "اتمام المهمة",
            moduleType= Dto.Enums.ModuleType.Email,


               },

               new NotificationTypeTemplate{
                   NameEnglish = "Delayed task",
                    IsEnable=true,
                   NameArabic = "تاخير المهمه",
                moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Pending review",
                    IsEnable=true,
                   NameArabic = "مراجعه موقفه",
               moduleType= Dto.Enums.ModuleType.Email,

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Request end date change",
                   IsEnable=true,
                   NameArabic = "طلب تغيير تاريخ الانهاء",
                  moduleType= Dto.Enums.ModuleType.Email,

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Approve or reject request end date change",
                 IsEnable=true,
                   NameArabic = "قبول او رفض تغيير تاريخ الانهاء",
                  moduleType= Dto.Enums.ModuleType.Email,

               },
                  new NotificationTypeTemplate{
                   NameEnglish = "Reject the end date change request",
                   IsEnable=true,
                   NameArabic = "رفض طلب تغيير تاريخ الانتهاء",
                   moduleType= Dto.Enums.ModuleType.Email,


               },
                   new NotificationTypeTemplate{
                   NameEnglish = "Weekly report",
                   IsEnable=true,
                   NameArabic = "تقرير اسبوعى",
                   moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Edit task",
                   IsEnable=true,
                   NameArabic = "تعديل المهمه",
                moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Reject task",
                   IsEnable=true,
                   NameArabic = "رفض المهمه",
                    moduleType= Dto.Enums.ModuleType.Email,

               },
                new NotificationTypeTemplate{
                   NameEnglish = "Change task priority",
                   IsEnable=true,
                   NameArabic = "تغيير اولويه المهمه",
                   moduleType= Dto.Enums.ModuleType.Email,

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Escalate task",
                     IsEnable=true,
                   NameArabic = "جدوله المهمه",
                moduleType= Dto.Enums.ModuleType.Email,


               }, new NotificationTypeTemplate{
                   NameEnglish = "Delete task ",
                     IsEnable=true,
                   NameArabic = "حذف المهمه",
                moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Mention user in comment",
                    IsEnable = true,
                   NameArabic = "تذكير مستخدم فى التعليقات",
                   moduleType= Dto.Enums.ModuleType.Email,

               }, new NotificationTypeTemplate{
                   NameEnglish = "Add delegation",
                   IsEnable = true, NameArabic = "اضافه تفويض",
                   moduleType= Dto.Enums.ModuleType.Email,

               },
               new NotificationTypeTemplate{
                   NameEnglish = "Delete delegation",
                   IsEnable=true,
                   NameArabic = "حذف تفويض",
                   moduleType= Dto.Enums.ModuleType.Email,

               }, new NotificationTypeTemplate{
                   NameEnglish = "Edit delegation",
                     IsEnable=true,
                   NameArabic = "تعديل تفويض",
                  moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Add Escalation",
                   IsEnable=true,
                   NameArabic = "اضافه جدوله",
                    moduleType= Dto.Enums.ModuleType.Email,

               }, new NotificationTypeTemplate{
                   NameEnglish = "Delete Escalation",
                     IsEnable=true,
                   NameArabic = "حذف جدوله",
                    moduleType= Dto.Enums.ModuleType.Email,


               },
               new NotificationTypeTemplate{
                   NameEnglish = "Edit Escalation",
                        IsEnable=true,
                   NameArabic = "تعديل جدوله",
                  moduleType= Dto.Enums.ModuleType.Email,

               }

            };

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
