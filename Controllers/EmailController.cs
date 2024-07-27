using Microsoft.AspNetCore.Mvc;
using EmailSenderAPI.Model;

namespace EmailSenderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : Controller
{
    private readonly EmailService _emailService;

    public EmailController(IConfiguration configuration)
    {
        _emailService = new EmailService(configuration);
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    [Route("SendEmail")]
    public async Task<IActionResult> SendEmail([FromForm] EmailModel emailRequest)
    {
        if (ModelState.IsValid)
        {
            await _emailService.SendEmailAsync(emailRequest.Recipient, emailRequest.Subject, emailRequest.Body);
            ViewBag.Message = "Email sent successfully!";
        }
        return View("Index");
    }
}

