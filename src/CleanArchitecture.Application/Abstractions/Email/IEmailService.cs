using System;

namespace CleanArchitecture.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, 
        CancellationToken cancellationToken = default);
    Task SendOrderConfirmationAsync(string to, string orderNumber, 
        CancellationToken cancellationToken = default);
}
