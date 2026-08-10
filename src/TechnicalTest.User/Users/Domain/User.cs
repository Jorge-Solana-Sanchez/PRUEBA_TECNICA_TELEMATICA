using System;
using System.Net.Mail;
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
        Created = DateTime.UtcNow;
        Name = ValidateName(name);
        Email = ValidateEmail(email);
    }

    [JsonConstructor]
    public User(Guid id, string name, string email, DateTime created)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(
                "El identificador del usuario no puede estar vacío.",
                nameof(id));
        }

        Id = id;
        Created = created;
        Name = ValidateName(name);
        Email = ValidateEmail(email);
    }

    public void UpdateUser(string name, string email)
    {
        Name = ValidateName(name);
        Email = ValidateEmail(email);
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "El nombre del usuario es obligatorio.",
                nameof(name));
        }

        return name.Trim();
    }

    private static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(
                "El email del usuario es obligatorio.",
                nameof(email));
        }

        email = email.Trim();

        try
        {
            var address = new MailAddress(email);

            if (!string.Equals(
                    address.Address,
                    email,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException();
            }
        }
        catch (FormatException)
        {
            throw new ArgumentException(
                "El email del usuario no es válido.",
                nameof(email));
        }

        return email;
    }
}