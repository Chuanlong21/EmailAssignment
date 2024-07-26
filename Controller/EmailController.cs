using Microsoft.AspNetCore.Mvc;

namespace EmailSenderAPI.Controller;


[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(IConfiguration configuration)
    {
        _emailService = new EmailService(configuration);
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
    {
        await _emailService.SendEmailAsync(emailRequest.Recipient, emailRequest.Subject, emailRequest.Body);
        return Ok("Email sent!");
    }
}

public class EmailRequest
{
    public string Recipient { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}