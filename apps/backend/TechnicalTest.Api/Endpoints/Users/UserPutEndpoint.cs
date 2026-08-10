using Microsoft.AspNetCore.Mvc;
using TechnicalTest.User.Users.Application.Updater;
using UserEntity = TechnicalTest.User.Users.Domain.User;

namespace TechnicalTest.Api.Endpoints.Users;

public record UpdateUserRequest(string Name, string Email);

public static class UsersPutEndpoint
{
    public static void MapUsersPutEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id:guid}", async (
                [FromRoute] Guid id,
                [FromBody] UpdateUserRequest request,
                [FromServices] UpdateUser.Handler handler,
                CancellationToken ct) =>
            {
                var command = new UpdateUser.Command(id, request.Name, request.Email);
                var updatedUser = await handler.Handle(command, ct);

                if (updatedUser is null)
                {
                    return Results.NotFound(new { Message = $"\"No se encontró el usuario con ID '{id}'" });
                }

                return Results.Ok(updatedUser);
            })
            .WithTags("Users")
            .WithName("UpdateUser")
            .Produces<UserEntity>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}