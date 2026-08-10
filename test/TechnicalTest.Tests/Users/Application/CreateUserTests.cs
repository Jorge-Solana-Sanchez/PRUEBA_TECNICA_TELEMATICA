using FluentAssertions;
using NSubstitute;
using TechnicalTest.User.Users.Application.Creator;
using TechnicalTest.User.Users.Application.Notifier;
using TechnicalTest.User.Users.Domain;
using UserEntity = TechnicalTest.User.Users.Domain.User;

namespace TechnicalTest.Tests.Users.Application;

[TestFixture]
public class CreateUserTests
{
    private IUserRepository _repositoryMock;
    private IEmailNotifier _emailNotifierMock;
    private CreateUser.Handler _handler;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = Substitute.For<IUserRepository>();
        _emailNotifierMock = Substitute.For<IEmailNotifier>();

        _handler = new CreateUser.Handler(_repositoryMock, _emailNotifierMock);
    }

    [Test]
    public async Task Should_Create_User_And_Send_Email()
    {
        var command = new CreateUser.Command("María López", "maria@example.com");

        var result = await _handler.Handle(command);

        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);   
        result.Email.Should().Be(command.Email); 
        result.Id.Should().NotBeEmpty();

        await _repositoryMock.Received(1).AddAsync(Arg.Any<UserEntity>());

        await _emailNotifierMock.Received(1).SendEmailAsync(result.Email, result.Name);
    }
}