using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Domain.Models.Task;

public class Department : Entity, IHasCreationMetaData, IHasUpdateMetaData
{
	public string NameEnglish { get; set; }
	public string NameArabic { get; set; }
	public Guid CreatedById { get; set; }
	public Guid? ParentdepartmentId { get; set; }
	public Department Parentdepartment { get; set; }
	public int Level { get; set; }
	public DateTime CreateDate { get; set; }
	public User CreatedBy { get; set; }
	public Guid? UpdatedById { get; set; }
	public DateTime? UpdateDate { get; set; }
	public User? UpdatedBy { get; set; }
	public ICollection<User>? AssignedUser { set; get; }
	public Department(Guid id, string nameEn, string nameAr, Guid createrid)
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
		NameArabic = departmentDto.NameArabic;
		//CreatedById = departmentDto.CreatedBy.Value;
		UpdatedById = departmentDto.UpdatedBy;
		ParentdepartmentId = departmentDto.ParentdepartmentId == Guid.Empty ? null : departmentDto.ParentdepartmentId;
		CreateDate = departmentDto.CreateDate ?? DateTime.UtcNow;
		return true;

	}
	public DepartmentDto CopyToDto()
	{
		return new DepartmentDto
		{
			Id = Id,
			NameArabic = NameArabic,
			NameEnglish = NameEnglish,
			CreatedBy = CreatedById,
			UpdatedBy = UpdatedById,
			CreateDate = CreateDate,
			UpdateDate = UpdateDate,
			ParentdepartmentId = ParentdepartmentId == null ? Guid.Empty : ParentdepartmentId.Value,
			ParentdepartmentArabic = Parentdepartment?.NameArabic,
			ParentdepartmentEnglish = Parentdepartment?.NameEnglish,
			Level = Level,
			AssignedUsers = AssignedUser?.Select(u => new UserDto { Id = u.Id }).ToList() ?? new List<UserDto>()
		};
	}
	public void Update(string nameEn, string nameAr)
	{
		NameEnglish = nameEn;
		NameArabic = nameAr;
	}
}