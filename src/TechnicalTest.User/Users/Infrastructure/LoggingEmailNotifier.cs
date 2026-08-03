using Microsoft.Extensions.Logging;
using TechnicalTest.User.Users.Application.Notifier;

namespace TechnicalTest.User.Users.Infrastructure;

public class LoggingEmailNotifier : IEmailNotifier
{
    private readonly ILogger<LoggingEmailNotifier> _logger;

    public LoggingEmailNotifier(ILogger<LoggingEmailNotifier> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string name, CancellationToken ct = default)
    {
        _logger.LogInformation(
            "[SIMULACIÓN EMAIL] Enviando email a {Name} ({Email})", name, email);
        
        return Task.CompletedTask;
    }
    
}