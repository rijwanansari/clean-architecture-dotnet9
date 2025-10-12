using System;
using CleanArchitecture.Application.Abstractions.Email;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body,
        CancellationToken cancellationToken = default)
    {
        // Simulate email sending
        _logger.LogInformation("Sending email to {To} with subject: {Subject}", to, subject);
        await Task.Delay(100, cancellationToken);
        _logger.LogInformation("Email sent successfully to {To}", to);
    }
    
    public async Task SendOrderConfirmationAsync(string to, string orderNumber, 
        CancellationToken cancellationToken = default)
    {
        var subject = $"Order Confirmation - {orderNumber}";
        var body = $"Thank you for your order! Your order number is {orderNumber}.";
        await SendEmailAsync(to, subject, body, cancellationToken);
    }

}
