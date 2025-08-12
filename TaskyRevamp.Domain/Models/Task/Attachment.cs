using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class Attachment : Entity
{
    private Attachment() { }
    public string FileName { get; private set; }
    public byte[] Content { get; private set; }
    public long Size => Content.LongLength;
    public User UploadedBy { get; private set; }
    public DateTime UploadedAt { get; private set; }
    public Attachment(Guid id, string fileName, byte[] content, User by)
    {
        Id = id;
        FileName = fileName;
        Content = content;
        UploadedBy = by;
        UploadedAt = DateTime.UtcNow;
    }
}