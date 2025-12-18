namespace TaskyRevamp.Client;

public class MySettings
{
    public int PageSize { get; set; }
    public long MaxFileSize { get; set; }
    public long MaxVideoSize { get; set; }
}

public class ValidationResult
{
    public bool NotValid { get; set; }
    public string ValidationMessage { get; set; }
}

public enum UploadedFileType
{
    Image = 1,
    Video,
    Excel
}
