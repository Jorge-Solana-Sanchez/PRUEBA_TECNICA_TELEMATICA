using FluentAssertions;
using UserEntity = TechnicalTest.User.Users.Domain.User;

namespace TechnicalTest.Tests.Users.Domain;

[TestFixture]
public class UserTests
{
    [Test]
    public void Should_Create_User_With_Valid_Data()
    {
        var user = new UserEntity(
            " Jorge Solana ",
            " jorge@example.com ");

        user.Name.Should().Be("Jorge Solana");
        user.Email.Should().Be("jorge@example.com");
        user.Id.Should().NotBeEmpty();
        user.Created.Should().NotBe(default);
    }

    [Test]
    public void Should_Throw_When_Name_Is_Empty()
    {
        var action = () => new UserEntity("", "jorge@example.com");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*nombre*obligatorio*");
    }

    [Test]
    public void Should_Throw_When_Email_Is_Empty()
    {
        var action = () => new UserEntity("Jorge Solana", "");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*email*obligatorio*");
    }

    [Test]
    public void Should_Throw_When_Email_Is_Invalid()
    {
        var action = () => new UserEntity("Jorge Solana", "email-invalido");

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("*email*no es válido*");
    }

    [Test]
    public void Should_Throw_When_Updating_With_Invalid_Data()
    {
        var user = new UserEntity(
            "Jorge Solana",
            "jorge@example.com");

        var action = () => user.UpdateUser("", "email-invalido");

        action.Should().Throw<ArgumentException>();
    }
}