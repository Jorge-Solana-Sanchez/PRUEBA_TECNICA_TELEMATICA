namespace TechnicalTest.User.Users.Domain;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id,  CancellationToken cancellationToken = default);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}