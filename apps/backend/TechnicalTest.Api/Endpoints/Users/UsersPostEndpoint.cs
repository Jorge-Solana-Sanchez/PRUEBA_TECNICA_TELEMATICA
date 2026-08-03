using Microsoft.AspNetCore.Mvc;
using UserEntity = TechnicalTest.User.Users.Domain.User;
using TechnicalTest.User.Users.Application.Creator;

namespace TechnicalTest.Api.Endpoints.Users;

public static class UsersPostEndpoint
{
    public static void MapUsersPostEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users", async (
                [FromBody] CreateUser.Command command,
                [FromServices] CreateUser.Handler handler,
                CancellationToken ct) =>
            {
                var createdUser = await handler.Handle(command, ct);

                return Results.Created($"/api/users/{createdUser.Id}", createdUser);
            })
            .WithTags("Users")
            .WithName("CreateUser")
            .Produces<UserEntity>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }
}