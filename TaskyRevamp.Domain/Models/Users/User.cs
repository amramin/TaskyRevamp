using TaskyRevamp.Domain.Interfaces;

namespace TaskyRevamp.Domain.Models.Users;

public class User : Entity
{
    public string? NameEnglish { get; set; }
    public string? NameArabic { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? DistinguishedName { get; set; }
    public string? GivenName { get; set; }
    public string? Mobile { get; set; }
    public bool IsActive { get; set; }
    public bool IsManager { get; set; }

    //public Department Department { get; set; }
    //public Privilege Privilege { get; set; }


}
