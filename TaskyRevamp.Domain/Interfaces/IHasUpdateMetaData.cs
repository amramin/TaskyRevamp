using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Interfaces;

public interface IHasUpdateMetaData
{
    public Guid? UpdatedById { get; set; }
    public DateTime? UpdateDate { get; set; }
    public User? UpdatedBy { get; set; }
}