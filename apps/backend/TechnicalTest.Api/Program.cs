using TechnicalTest.Api.Endpoints.Users;
using TechnicalTest.User.Users.Application.Creator;
using TechnicalTest.User.Users.Application.Finder;
using TechnicalTest.User.Users.Application.Notifier;
using TechnicalTest.User.Users.Application.Updater;
using TechnicalTest.User.Users.Domain;
using TechnicalTest.User.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios de OpenAPI y Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var usersFilePath = Path.Combine(
    builder.Environment.ContentRootPath,
    "Data",
    "users.json");

builder.Services.AddScoped<IUserRepository>(
    _ => new JsonUserRepository(usersFilePath));

builder.Services.AddScoped<SearchUsers.Handler>();
builder.Services.AddScoped<IEmailNotifier, LoggingEmailNotifier>();
builder.Services.AddScoped<CreateUser.Handler>();
builder.Services.AddScoped<UpdateUser.Handler>();

var app = builder.Build();

// Habilitar Swagger UI siempre para probar fácilmente
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Technical Test API v1");
    c.RoutePrefix = "swagger"; // La interfaz estará en /swagger
});

app.UseHttpsRedirection();

UsersGetEndpoint.MapEndpoint(app);
app.MapUsersPostEndpoint();
app.MapUsersPutEndpoint();

app.Run();