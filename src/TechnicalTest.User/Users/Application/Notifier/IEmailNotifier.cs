namespace TechnicalTest.User.Users.Application.Notifier;

public interface IEmailNotifier
{
    Task SendEmailAsync(string email, string name, CancellationToken ct = default);
}