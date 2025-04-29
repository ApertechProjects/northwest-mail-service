
using System.Net;
using System.Net.Mail;
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

        foreach (var to in request.MailMessage.To)
        {
            message.To.Add(to);
        }

        smtpClient.Send(message);
    }
}