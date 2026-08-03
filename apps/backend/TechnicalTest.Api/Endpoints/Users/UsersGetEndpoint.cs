using Microsoft.AspNetCore.Mvc;
using TechnicalTest.User.Users.Application.Finder;
using UserEntity = TechnicalTest.User.Users.Domain.User;

namespace TechnicalTest.Api.Endpoints.Users;

public static class UsersGetEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async (
                [FromServices] SearchUsers.Handler handler,
                CancellationToken ct) =>
            {
                var query = new SearchUsers.Query();
                var users = await handler.Handle(query, ct);
                
                return Results.Ok(users);
            })
            .WithTags("Users")
            .WithName("GetUsers")
            .Produces<IEnumerable<UserEntity>>(StatusCodes.Status200OK);
    }
}