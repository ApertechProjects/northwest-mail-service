namespace HRIntegrationService.Models;

public class MailMessageDto
{
    public List<string> To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsBodyHtml { get; set; }
    public List<string> Attachment { get; set; }
    public List<string> AttachmentName { get; set; }
    public string DisplayName { get; set; }
}