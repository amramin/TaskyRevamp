using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.Account;

namespace TaskyRevamp.Domain.Models.Users;

public class User : Entity
{
    public string? Username { get; set; }
    public string? NameEnglish { get; set; }
    public string? NameArabic { get; set; }
    public string? Email { get; set; }
    public string? DistinguishedName { get; set; }
    public string? GivenName { get; set; }
    public string? Mobile { get; set; }
    public bool IsActive { get; set; }
    public bool IsManager { get; set; }
    public Guid? DepartmentId { get; set; }
    public Department? Department { get;  set; }
    public User()
    {

    }
    public User(Guid id, string userName, Department dept, User by)
    {
        Id = id;
        Username = userName;
        Department = dept;
    }
    public void Update(string userName, Department dept, User by)
    {
        Username = userName;
        Department = dept;

    }

    public virtual UserDto CopyToDto()
    {
        UserDto dto = new UserDto()
        {
            Id = Id,
            userNameAR = NameArabic,
            userNameEN = NameEnglish,
            Email = Email,
        };

        return dto;
    }
}