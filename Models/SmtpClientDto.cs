namespace HRIntegrationService.Models;

public class SmtpClientDto
{
    public int Port { get; set; }
    public string Host { get; set; }
    public bool EnableSSL { get; set; }
    public string Sender { get; set; }
    public string Password { get; set; }
}
