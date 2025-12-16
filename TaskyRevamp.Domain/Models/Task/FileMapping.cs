namespace TaskyRevamp.Domain.Models.UploadFile;

public class FileMapping : Entity
{
    public Guid FileId { get; set; }
    public string FileName { get; set; }
    public FileType FileType { get; set; }
}