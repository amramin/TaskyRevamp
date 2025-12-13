
namespace TaskyRevamp.Dto;

public class AppSettings
{
    public string? ExpiryHours { get; set; }
    public string Secret { get; set; }
    public string BaseUploadDirectory { get; set; }

    public EmailSettings EmailSettings { get; set; }
    public int AuthenticationMode { get; set; }

    public string NetworkUsername { get; set; }
    public string NetworkPassword { get; set; }
    public string NetworkDomain { get; set; }

}
public class EmailSettings
{
    public string SMTPAddress { get; set; }
    public int SMTPPort { get; set; }
    public string Email { get; set; }
    public string SenderName { get; set; }
    public string Password { get; set; }
    public string LoginUrl { get; set; }
}
