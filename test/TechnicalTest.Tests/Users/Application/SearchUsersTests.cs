using UserEntity = TechnicalTest.User.Users.Domain.User;
using FluentAssertions;
using NSubstitute;
using TechnicalTest.User.Users.Application.Finder;
using TechnicalTest.User.Users.Domain;

namespace TechnicalTest.Tests.Users.Application;

[TestFixture]
public class SearchUsersTests
{
    private IUserRepository _repositoryMock = null!;
    private SearchUsers.Handler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = Substitute.For<IUserRepository>();
        _handler = new SearchUsers.Handler(_repositoryMock);
    }

    [Test]
    public async Task Handle_ShouldReturnAllUsersFromRepository()
    {
        var expectedUsers = new List<UserEntity>
        {
            new UserEntity(Guid.NewGuid(), "Carlos Pérez", "carlos@example.com", DateTime.UtcNow),
            new UserEntity(Guid.NewGuid(), "Laura Gómez", "laura@example.com", DateTime.UtcNow)
        };

        _repositoryMock
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<IEnumerable<UserEntity>>(expectedUsers));

        var query = new SearchUsers.Query();

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(expectedUsers);

        await _repositoryMock.Received(1).GetAllAsync(Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_WhenNoUsersExist_ShouldReturnEmptyList()
    {
        _repositoryMock
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<IEnumerable<UserEntity>>([]));

        var query = new SearchUsers.Query();

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeEmpty();
    }
}