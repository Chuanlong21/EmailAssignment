
using System.Net;
using System.Net.Mail;


namespace EmailSenderAPI;

public class EmailService
{
    private readonly IConfiguration _configuration;
    private const int MaxRetries = 3;
    
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendEmailAsync(string recipient, string subject, string body)
    {
        var emailConfig = _configuration.GetSection("EmailSettings").Get<EmailSettings>();
        int attempt = 0;
        bool sent = false;

        while (attempt < MaxRetries && !sent)
        {
            try
            {
                using var smtpClient = new SmtpClient(emailConfig.SmtpServer)
                {
                    Port = emailConfig.Port,
                    Credentials = new NetworkCredential(emailConfig.Username, emailConfig.Password),
                    EnableSsl = emailConfig.EnableSsl,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailConfig.FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(recipient);

                await smtpClient.SendMailAsync(mailMessage);
                sent = true;
                LogEmail(recipient, subject, body, "Sent");
            }
            catch (Exception ex)
            {
                attempt++;
                LogEmail(recipient, subject, body, $"Failed: {ex.Message}");
                if (attempt >= MaxRetries)
                {
                    throw;
                }
            }
        }
    }
    
    private void LogEmail(string recipient, string subject, string body, string status)
    {
        // Implement logging logic here (e.g., write to a file or database)
    }

    
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string FromEmail { get; set; }
    }
}