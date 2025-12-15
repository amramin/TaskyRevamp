using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskAttachment;

namespace TaskyRevamp.Domain.Models.Task;

public class Attachment : Entity, IHasCreationMetaData
{
    public string FileName { get; private set; }
    public byte[] Content { get; private set; }
    public long Size => Content.LongLength;
	public Guid TaskAttachmentId { get; set; }
	public TaskAttachments TaskAttachment { get; set; }
	public Guid CreatedById { get; set; }
	public DateTime CreateDate { get; set; }
	public User CreatedBy { get; set; }

	private Attachment() { }
	public Attachment(Guid id, string fileName, byte[] content, Guid taskAttachmentId)
    {
        Id = id;
        FileName = fileName;
        Content = content;
        TaskAttachmentId = taskAttachmentId;
    }

    public AttachmentDto CopyToDto()
    {
        return new AttachmentDto()
        {
            Id = Id,
            TaskAttachmentId = TaskAttachmentId,
            FileName = FileName,
            Content = Content,
            CreatedById = CreatedById,
            CreateDate = CreateDate
        };
	}
}