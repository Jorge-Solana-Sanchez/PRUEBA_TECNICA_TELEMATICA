using FluentAssertions;
using NSubstitute;
using TechnicalTest.User.Users.Application.Updater;
using TechnicalTest.User.Users.Domain;
using UserEntity = TechnicalTest.User.Users.Domain.User;

namespace TechnicalTest.Tests.Users.Application;

[TestFixture]
public class UpdateUserTests
{
    private IUserRepository _repositoryMock;
    private UpdateUser.Handler _handler;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = Substitute.For<IUserRepository>();
        _handler = new UpdateUser.Handler(_repositoryMock);
    }

    [Test]
    public async Task Should_Update_User_When_User_Exists()
    {
        var existingUser = new UserEntity("Pedro Marmol", "pedro@example.com");
        _repositoryMock.GetByIdAsync(existingUser.Id).Returns(existingUser);

        var command = new UpdateUser.Command(existingUser.Id, "Pedro Nuevo", "pedro.nuevo@example.com");

        var result = await _handler.Handle(command);

        result.Should().NotBeNull();
        result!.Name.Should().Be(command.Name);
        result.Email.Should().Be(command.Email);

        await _repositoryMock.Received(1).UpdateAsync(Arg.Any<UserEntity>());
    }

    [Test]
    public async Task Should_Return_Null_When_User_Does_Not_Exist()
    {
        
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.GetByIdAsync(nonExistingId).Returns((UserEntity?)null);

        var command = new UpdateUser.Command(nonExistingId, "Nombre", "email@example.com");
        
        var result = await _handler.Handle(command);

        result.Should().BeNull();
        
        await _repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<UserEntity>());
    }
}