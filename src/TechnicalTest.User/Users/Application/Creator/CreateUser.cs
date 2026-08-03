using UserEntity = TechnicalTest.User.Users.Domain.User;
using TechnicalTest.User.Users.Application.Notifier;
using TechnicalTest.User.Users.Domain;

namespace TechnicalTest.User.Users.Application.Creator;

public static class CreateUser
{
    public sealed record Command(string name, string email);

    public sealed class Handler
    {
        private readonly IUserRepository _repository;
        private readonly IEmailNotifier _emailNotifier;

        public Handler(IUserRepository repository, IEmailNotifier emailNotifier)
        {
            _repository = repository;
            _emailNotifier = emailNotifier;
        }

        public async Task<UserEntity> Handle(Command command, CancellationToken ct = default)
        {
            var user = new UserEntity(command.name, command.email);
            
            await _repository.AddAsync(user);
            await _emailNotifier.SendEmailAsync(user.Email, user.Name, ct);

            return user;
        }

    }
}