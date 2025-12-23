using System.Drawing;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskAttachment;

namespace TaskyRevamp.Domain.Models.Task;

public class Attachment : Entity, IHasCreationMetaData
{
    public string FileName { get; private set; }
	public Guid FileId { get; private set; }
	public long Size { get; private set; }
    public FileType FileType { get; set; }
	public Guid TaskAttachmentId { get; set; }
	public TaskAttachments TaskAttachment { get; set; }
	public Guid CreatedById { get; set; }
	public DateTime CreateDate { get; set; }
	public User CreatedBy { get; set; }

	private Attachment() { }
	public Attachment(Guid id, string fileName, Guid fileId, long size, Guid taskAttachmentId, FileType fileType)
    {
        Id = id;
        FileName = fileName;
        FileId = fileId;
		TaskAttachmentId = taskAttachmentId;
        Size = size;
        FileType = fileType;
	}

    public AttachmentDto CopyToDto()
    {
        return new AttachmentDto()
        {
            Id = Id,
            TaskAttachmentId = TaskAttachmentId,
            FileName = FileName,
            FileId = FileId,
			Size = Size,
            CreatedById = CreatedById,
            CreateDate = CreateDate
        };
	}
}