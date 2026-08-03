using TechnicalTest.User.Users.Domain;
using UserEntity = TechnicalTest.User.Users.Domain.User;

namespace TechnicalTest.User.Users.Application.Finder;

public static class SearchUsers
{
    public sealed record Query;

    public sealed class Handler(IUserRepository repository)
    {
        public async Task<IEnumerable<UserEntity>> Handle(Query query, CancellationToken cancellationToken = default)
        {
            return await repository.GetAllAsync(cancellationToken);
        }
    }
}