using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskyRevamp.Domain;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.UploadFile;
using TaskyRevamp.Dto;
using TaskyRevamp.Dto.Files;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Infrastructure;

public class FileManagement : IFileManagement
{
    private readonly EfDbContext context;
    private readonly IOptions<AppSettings> _appSettingsOptions;
    private string baseDirectory = "/";
    private readonly string[] _validExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx", ".mp4", ".avi", ".mov", ".mkv", ".webm", ".flv", ".wmv", ".mpeg", ".mpg", ".xlsx", ".xls", ".xlsm" };
    private readonly string _networkUsername;
    private readonly string _networkPassword;
    private readonly string _networkDomain;
    public FileManagement(EfDbContext _context, IOptions<AppSettings> appSettingsOptions)
    {
        context = _context;
        _appSettingsOptions = appSettingsOptions;
        baseDirectory = _appSettingsOptions.Value.BaseUploadDirectory;

        // Set network credentials
        _networkUsername = _appSettingsOptions.Value.NetworkUsername;
        _networkPassword = _appSettingsOptions.Value.NetworkPassword;
        _networkDomain = _appSettingsOptions.Value.NetworkDomain;
    }
    public async Task<List<FileDto>> GetFilesInfo(List<Guid> fileIds)
    {
        return await Impersonate(() => GetFilesInfoInternal(fileIds));
    }

    public async Task<List<FileDto>> GetFilesInfoInternal(List<Guid> fileIds)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        List<FileDto> answer = new();
        var files = await context.Attachment.Where(x => fileIds.Contains(x.FileId)).ToListAsync();
        var downloadedFiles = await DownloadFiles(fileIds);

        foreach (var file in files)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (file != null)
            {
                FileDto fileDto = new();
                fileDto.Id = file.FileId;
                fileDto.Name = file.FileName;
                fileDto.FileBase64 = downloadedFiles.Where(x => x.fileId == file.FileId).FirstOrDefault().base64;
                fileDto.Extention = fileExtension;
                answer.Add(fileDto);
            }
        }


        stopWatch.Stop();
        Console.WriteLine("Inside GetFilesInfo: " + stopWatch.ElapsedMilliseconds);


        return answer;
    }

    public async Task<FileDto> GetFileInfo(Guid fileId)
    {
        FileDto answer = null;
        var file = context.Attachment.FirstOrDefault(x => x.FileId == fileId);
        if (file != null)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            answer = new FileDto();
            answer.Name = file.FileName;
            answer.FileBase64 = await DownloadFile(fileId);
            answer.Extention = fileExtension;
        }

        return answer;
    }

    public async Task<string> GetFileName(Guid fileId)
    {
        var file = context.Attachment.FirstOrDefault(x => x.FileId == fileId);
        if (file != null)
        {
            return file.FileName;
        }

        return null;
    }

    public async Task<Guid> UploadFile(List<byte> bytes, string fileName, FileType fileType, Guid? fileId = null)
    {
        var byteArray = bytes.ToArray();
        return await UploadFile(byteArray, fileName, fileType, fileId);
    }

    public async Task<Guid> UploadFile(string base64, string fileName, FileType fileType, Guid? fileId = null)
    {
        if (string.IsNullOrEmpty(base64) || string.IsNullOrWhiteSpace(base64))
        {
            return Guid.Empty;
        }

        var bytes = Convert.FromBase64String(base64);
        return await UploadFile(bytes, fileName, fileType, fileId);
    }

    public async Task<Guid> UploadFile(byte[] bytes, string fileName, FileType fileType, Guid? fileId = null)
    {
        return await Impersonate(() => UploadFileInternal(bytes, fileName, fileType, fileId));
    }

    private async Task<Guid> UploadFileInternal(byte[] bytes, string fileName, FileType fileType, Guid? fileId = null)
    {
        
        var directory = Path.Combine(baseDirectory, fileType.ToString());
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (fileId != null && fileId != Guid.Empty)
        {
            var fileExist = CheckFileExist(fileId.Value, fileType);
            if (fileExist)
            {
                await DeleteFile(fileId.Value, fileType);
            }
        }

        var newFileId = fileId is null || fileId == Guid.Empty ? Guid.NewGuid() : fileId.Value;
        var path = Path.Combine(directory, $"{newFileId}");
        await System.IO.File.WriteAllBytesAsync(path, bytes);
        
        return newFileId;
    }

    public async Task<List<(Guid fileId, string base64)>> DownloadFiles(List<Guid> fileIds)
    {
        List<(Guid fileId, string base64)> results = new();
        var files = await context.Attachment.Where(x => fileIds.Contains(x.FileId)).ToListAsync();
        foreach (var file in files)
        {
            if (file is not null)
            {
                var directory = Path.Combine(baseDirectory, file.FileType.ToString());
                var path = Path.Combine(directory, file.FileId.ToString());

                if (!File.Exists(path))
                {
                    results.Add((file.FileId, "")); // Or throw an exception if you prefer
                    continue;
                }

                var bytes = await File.ReadAllBytesAsync(path);
                var result = Convert.ToBase64String(bytes);
                results.Add((file.FileId, result));
            }
        }

        return results;
    }

    public async Task<string> DownloadFile(Guid fileId)
    {
        var result = "";
        var file = context.Attachment.FirstOrDefault(x => x.FileId == fileId);
        if (file is not null)
        {
            var directory = Path.Combine(baseDirectory, file.FileType.ToString());
            var path = Path.Combine(directory, fileId.ToString());

            if (!File.Exists(path))
            {
                return result; // Or throw an exception if you prefer
            }

            var bytes = await File.ReadAllBytesAsync(path);
            result = Convert.ToBase64String(bytes);
        }

        return result;
    }
	public async Task<byte[]> DownloadFileAsBytes(Guid fileId)
	{
		var file = context.Attachment.FirstOrDefault(x => x.FileId == fileId);
		if (file is null)
			throw new FileNotFoundException("File not found");

		var directory = Path.Combine(baseDirectory, file.FileType.ToString());
		var path = Path.Combine(directory, fileId.ToString());

		if (!File.Exists(path))
			throw new FileNotFoundException("File not found on disk");

		return await File.ReadAllBytesAsync(path);
	}
	public bool CheckFileExist(Guid fileId, FileType fileType)
    {
        var directory = Path.Combine(baseDirectory, fileType.ToString());
        var path = Path.Combine(directory, fileId.ToString());

        // Check if the file already exists
        return File.Exists(path);
    }

    public async Task<Guid> DeleteFile(Guid fileId, FileType fileType)
    {
        var directory = Path.Combine(baseDirectory, fileType.ToString());
        var path = Path.Combine(directory, fileId.ToString());
        File.Delete(path);
        var file = context.Attachment.FirstOrDefault(x => x.FileId == fileId);
        if (file is not null)
        {
            context.Attachment.Remove(file);
            await context.SaveChangesAsync();
        }

        return fileId;
    }

    public List<FileDto> GetAllFiles()
    {
        var files = context.Attachment.ToList();
        if (files != null)
        {
            return files.Select(x => new FileDto { Id = x.FileId, Name = x.FileName }).ToList();
        }

        return new List<FileDto>();
    }

	public FileType ResolveFileType(string fileName)
	{
		var ext = Path.GetExtension(fileName)?.ToLower();

		return ext switch
		{
			".jpg" or ".jpeg" or ".png" or ".gif" => FileType.Images,
			".mp4" or ".avi" or ".mov" or ".mkv" => FileType.Videos,
			".svg" or ".ico" => FileType.Icons,
			_ => FileType.Files
		};
	}
	private async Task<T> Impersonate<T>(Func<Task<T>> action)
    {
        using (new NetworkShareAccess(_networkUsername, _networkPassword, _networkDomain))
        {
            return await action();
        }
    }

	
}