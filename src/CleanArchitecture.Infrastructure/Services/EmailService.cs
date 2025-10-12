using System.Threading.Tasks;
using CleanArchitecture.Application.Abstractions.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Services;

public class EmailService : IEmailService
{
    private const int EmailSendDelayMilliseconds = 100;
    private readonly ILogger<EmailService> _logger;
    private readonly IConfiguration _configuration;

    public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body,
        CancellationToken cancellationToken = default)
    {
        // Simulate email sending
        _logger.LogInformation("Sending email to {To} with subject: {Subject}", to, subject);
        await Task.Delay(EmailSendDelayMilliseconds, cancellationToken);
        _logger.LogInformation("Email sent successfully to {To}", to);
    }
    
    public async Task SendOrderConfirmationAsync(string to, string orderNumber, 
        CancellationToken cancellationToken = default)
    {
        var template = _configuration["EmailTemplates:OrderConfirmation"] 
            ?? "Thank you for your order! Your order number is {OrderNumber}.";
        var body = template.Replace("{OrderNumber}", orderNumber);
        var subject = "Order Confirmation";
        await SendEmailAsync(to, subject, body, cancellationToken);
    }

}
