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

    public async Task AddAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        var users = (await GetAllAsync()).ToList();
        users.Add(user);
        
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonText = JsonSerializer.Serialize(users, options);

        await File.WriteAllTextAsync(_filePath, jsonText);
    }

    public async Task UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        var users = (await GetAllAsync()).ToList();
        
        var index = users.FindIndex(u => u.Id == user.Id);

        if (index != -1)
        {
            users[index] = user;
            
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonText = JsonSerializer.Serialize(users, options);
            
            await File.WriteAllTextAsync(_filePath, jsonText);
        }
            
    }
}