using TechnicalTest.User.Users.Domain;
using UserEntity = TechnicalTest.User.Users.Domain.User;

namespace TechnicalTest.User.Users.Application.Updater;

public static class UpdateUser
{
    public sealed record Command(Guid Id, string Name, string Email);
    
    public sealed class Handler
    {
        private readonly IUserRepository _repository;

        public Handler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserEntity?> Handle(Command command, CancellationToken ct = default)
        {
            var user = await _repository.GetByIdAsync(command.Id, ct);

            if (user == null)
            {
                return null;
            }
            
            user.UpdateUser(command.Name, command.Email);

            await _repository.UpdateAsync(user);
            
            return user;
        }
    }
}