using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Interfaces;

public interface IHasCreationMetaData
{
    public Guid CreatedById { get; set; }
    public DateTime CreateDate { get; set; }
    public User CreatedBy { get; set; }
}