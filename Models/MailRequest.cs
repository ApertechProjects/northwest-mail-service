namespace HRIntegrationService.Models;

public class MailRequest
{
    public MailMessageDto MailMessage  { get; set; }
    public SmtpClientDto SmtpClient { get; set; }
}