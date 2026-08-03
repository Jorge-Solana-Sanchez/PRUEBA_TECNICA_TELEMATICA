using System.Text.Json;
using TechnicalTest.User.Users.Domain;
using UserEntity = TechnicalTest.User.Users.Domain.User;

namespace TechnicalTest.User.Users.Infrastructure;

public class JsonUserRepository : IUserRepository
{
    private readonly string _filePath;
    
    public JsonUserRepository(string filePath = "users.json")
        {
        _filePath = filePath;
        }

    public async Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_filePath))
        {
            return Enumerable.Empty<UserEntity>();
        }
        
        using var stream = File.OpenRead(_filePath);
        var users = await JsonSerializer.DeserializeAsync<List<UserEntity>>(stream, cancellationToken: cancellationToken);

        return users ?? Enumerable.Empty<UserEntity>();

    }
    
    public async Task<UserEntity?> GetByIdAsync(Guid id,  CancellationToken cancellationToken = default)
    {
        var users = await GetAllAsync();
        return users.FirstOrDefault(u => u.Id == id);
    }

    public Task AddAsync(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserEntity user)
    {
        throw new NotImplementedException();
    }
}