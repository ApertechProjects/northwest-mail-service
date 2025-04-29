using HRIntegrationService.Models;
using HRIntegrationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRIntegrationService.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MailController(MailService mailService)
{
    [HttpPost]
    public void GetEmployeesAsync([FromBody] MailRequest mailRequest)
    {
        mailService.SendMail(mailRequest);
    }
}