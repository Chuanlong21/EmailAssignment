namespace EmailSenderAPI;
class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var emailService = new EmailService(configuration);

            Console.WriteLine("Enter recipient email:");
            string recipient = Console.ReadLine();

            Console.WriteLine("Enter subject:");
            string subject = Console.ReadLine();

            Console.WriteLine("Enter body:");
            string body = Console.ReadLine();

            await emailService.SendEmailAsync(recipient, subject, body);
            Console.WriteLine("Email sent!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
}