namespace TaskyRevamp.Dto.GeneralDto;

public class ApiErrorDto
{
    public string Title { get; set; }
    public int Status { get; set; }
    public string Detail { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }
}