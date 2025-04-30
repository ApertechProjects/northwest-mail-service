
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using HRIntegrationService.Models;

namespace HRIntegrationService.Services;

public class MailService()
{
    public void SendMail(MailRequest request)
    {
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Port = request.SmtpClient.Port;
        smtpClient.Host = request.SmtpClient.Host;
        smtpClient.EnableSsl = request.SmtpClient.EnableSSL;
        smtpClient.Credentials = new NetworkCredential(request.SmtpClient.Sender, request.SmtpClient.Password);

        MailMessage message = new MailMessage();
        message.From = new MailAddress(request.SmtpClient.Sender, request.MailMessage.DisplayName);
        message.Subject = request.MailMessage.Subject;
        message.Body = request.MailMessage.Body;
        message.IsBodyHtml = request.MailMessage.IsBodyHtml;

        for (int i = 0; i < request.MailMessage.Attachment.Count; i++)
        {
            string base64String = request.MailMessage.Attachment[i];
            string fileName = request.MailMessage.AttachmentName[i];
            byte[] fileBytes = Convert.FromBase64String(base64String);
            MemoryStream stream = new MemoryStream(fileBytes);
            string mimeType = GetMimeType(fileName);

            Attachment attachment = new Attachment(stream, fileName, mimeType);
            message.Attachments.Add(attachment);
        }

        foreach (var to in request.MailMessage.To)
        {
            message.To.Add(to);
        }
        
        foreach (var cc in request.MailMessage.CC)
        {
            message.CC.Add(cc);
        }

        smtpClient.Send(message);
    }
    
    private string GetMimeType(string fileName)
    {
        string extension = Path.GetExtension(fileName).ToLower();

        return extension switch
        {
            ".pdf" => MediaTypeNames.Application.Pdf,
            ".txt" => MediaTypeNames.Text.Plain,
            ".html" => MediaTypeNames.Text.Html,
            ".jpg" or ".jpeg" => MediaTypeNames.Image.Jpeg,
            ".png" => "image/png",
            ".gif" => MediaTypeNames.Image.Gif,
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".csv" => "text/csv",
            _ => "application/octet-stream", // bilinmeyen tip
        };
    }
}