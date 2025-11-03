using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DisasterAlleviationApp.Services
{
    // Simple IEmailSender implementation for development.
    // Replace with a real provider (SendGrid/SMTP) in production.
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        public EmailSender(ILogger<EmailSender> logger) => _logger = logger;

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("Emulated sending email to {Email} with subject {Subject}. Message: {Message}", email, subject, htmlMessage);
            return Task.CompletedTask;
        }
    }
}
