using MediatR;
using Microsoft.Extensions.Options;

using TaskyRevamp.Dto.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace TaskyRevamp.Services.Email.Command;

public record SendMailCommand(SendEmailDto mailData) : IRequest<bool>;

public class SendMailHandler : IRequestHandler<SendMailCommand, bool>
{
    private readonly MailSettings _mailSettings;
    public SendMailHandler(IOptions<MailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }
    public async Task<bool> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        using var emailMessage = new MimeMessage();

        // Validate From
        if (!MailboxAddress.TryParse(_mailSettings.SenderEmail, out var emailFrom))
            throw new ArgumentException($"Invalid sender email: {_mailSettings.SenderEmail}");
        emailMessage.From.Add(new MailboxAddress(_mailSettings.SenderName, emailFrom.Address));

        // Validate To
        if (!MailboxAddress.TryParse(request.mailData.EmailToId, out var emailTo))
            throw new ArgumentException($"Invalid recipient email: {request.mailData.EmailToId}");
        emailMessage.To.Add(new MailboxAddress(request.mailData.EmailToName ?? string.Empty, emailTo.Address));

        // Subject & Body
        emailMessage.Subject = request.mailData.EmailSubject ?? "(No Subject)";
        var emailBodyBuilder = new BodyBuilder { HtmlBody = request.mailData.EmailBody ?? string.Empty };
        emailMessage.Body = emailBodyBuilder.ToMessageBody();

        using var mailClient = new MailKit.Net.Smtp.SmtpClient();
        try
        {
            await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);
            await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password, cancellationToken);
            await mailClient.SendAsync(emailMessage, cancellationToken);
            await mailClient.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Email Error] {ex}");
            return false;
        }

        return true;
    }


}