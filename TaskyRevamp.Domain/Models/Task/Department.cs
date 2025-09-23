using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Domain.Models.Task;

public class Department : Entity, IHasCreationMetaData, IHasUpdateMetaData
{
    public string NameEnglish { get;  set; }
    public string NameArabic { get; set; }
    public Guid CreatedById { get ; set ; }
    public DateTime CreateDate { get ; set ; }
    public User CreatedBy { get ; set ; }
    public Guid? UpdatedById { get ; set ; }
    public DateTime? UpdateDate { get ; set ; }
    public User? UpdateddBy { get ; set ; }
    

    public Department(Guid id, string nameEn,string nameAr,Guid createrid)
    {
        Id = id;
        NameEnglish = nameEn;
        NameArabic = nameAr;  
        CreatedById = createrid;
    }

    public Department()
    {
    }
    public bool SetData(DepartmentDto departmentDto)
    {
        Id = departmentDto.Id;
        NameEnglish = departmentDto.NameEnglish;
        NameArabic= departmentDto.NameArabic;
        CreatedById = departmentDto.CreatedBy;
        UpdatedById = departmentDto.UpdatedBy;
        CreateDate=departmentDto.CreateDate??DateTime.Now;
        return true;

    }
    public DepartmentDto CopyToDto()
    {
        return new DepartmentDto
        {
            Id = Id,
           NameArabic= NameArabic,
           NameEnglish = NameEnglish,
           CreatedBy= CreatedById,
           UpdatedBy= UpdatedById,
           CreateDate= CreateDate,
           UpdateDate= UpdateDate,


        };
    }
    public void Update(string nameEn,string nameAr)
    {
        NameEnglish = nameEn;
        NameArabic = nameAr;
        
    }
}