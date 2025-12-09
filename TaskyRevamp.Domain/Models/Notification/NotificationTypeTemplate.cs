using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Notification;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Domain.Models.Notification;

public class NotificationTypeTemplate : Entity
{
    public string NameEnglish { get; set; }
    public string NameArabic { get; set; }
    public string? TemplateEnglish { get; set; }
    public string? TemplateArabic { get; set; }
    public bool IsEnable { get; set; }
    public ModuleType moduleType { get; set; }
    public string? SubjectEnglish { get; set; }
    public string? SubjectArabic { get; set; }
    public NotificationTypeTemplate() { }

    public bool SetData(NotificationTypeTemplateDto typeTemplateDto)
    {
        Id = typeTemplateDto.Id;
        NameEnglish = typeTemplateDto.NameEnglish;
        NameArabic = typeTemplateDto.NameArabic;
        SubjectArabic = typeTemplateDto.SubjectArabic;
        SubjectEnglish = typeTemplateDto.SubjectEnglish;
        TemplateEnglish = typeTemplateDto.TemplateEnglish;
        IsEnable = typeTemplateDto.IsEnable;
        moduleType = typeTemplateDto.moduleType;
        TemplateArabic = typeTemplateDto.TemplateArabic;
        moduleType = typeTemplateDto.moduleType;
        return true;

    }
    public NotificationTypeTemplateDto CopyToDto()
    {
        return new NotificationTypeTemplateDto
        {
            Id = Id,
            NameArabic = NameArabic,
            NameEnglish = NameEnglish,
            SubjectEnglish = SubjectEnglish,
            TemplateEnglish = TemplateEnglish,
            SubjectArabic = SubjectArabic,
            moduleType = moduleType,
            IsEnable = IsEnable,
            TemplateArabic = TemplateArabic
        };
    }

}