using System;
using System.Text.Json.Serialization;

namespace TechnicalTest.User.Users.Domain;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime Created { get; private set; }

    public User(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Created = DateTime.UtcNow;
    }

    [JsonConstructor]
    public User(Guid id, string name, string email, DateTime created)
    {
        Id = id;    
        Name = name;
        Email = email;
        Created = created;
    }
    
}