using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Domain.Models.Task;

public class Department : Entity
{
    public string NameEnglish { get;  set; }
    public string NameArabic { get; set; }

    public Department(Guid id, string nameEn,string nameAr)
    {
        Id = id;
        NameEnglish = nameEn;
        NameArabic = nameAr;    
    }

    public Department()
    {
    }
    public bool SetData(DepartmentDto departmentDto)
    {
        Id = departmentDto.Id;
        NameEnglish = departmentDto.NameEnglish;
        NameArabic= departmentDto.NameArabic;
        return true;

    }
    public DepartmentDto CopyToDto()
    {
        return new DepartmentDto
        {
            Id = Id,
           NameArabic= NameArabic,
           NameEnglish = NameEnglish,




        };
    }
    public void Update(string nameEn,string nameAr)
    {
        NameEnglish = nameEn;
        NameArabic = nameAr;
        
    }
}