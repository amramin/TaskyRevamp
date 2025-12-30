using TaskyRevamp.Dto;
using TaskyRevamp.Dto.Files;

namespace TaskyRevamp.Domain.Interfaces;

public interface IFileManagement
{
    public Task<Guid> UploadFile(List<byte> bytes, string fileName, FileType fileType, Guid? fileId = null);
    public Task<Guid> UploadFile(byte[] bytes, string fileName, FileType fileType, Guid? fileId = null);
    public Task<Guid> UploadFile(string base64, string fileName, FileType fileType, Guid? fileId = null);
    public Task<string> DownloadFile(Guid fileId);
	public Task<byte[]> DownloadFileAsBytes(Guid fileId);
	public Task<string> GetFileName(Guid fileId);
    public Task<FileDto> GetFileInfo(Guid fileId);
    public List<FileDto> GetAllFiles();
    Task<List<FileDto>> GetFilesInfo(List<Guid> fileIds);
    Task<List<FileDto>> GetFilesInfoInternal(List<Guid> fileIds);
    public FileType ResolveFileType(string fileName);
	public Task<Guid> DeleteFile(Guid fileId, FileType fileType);
}