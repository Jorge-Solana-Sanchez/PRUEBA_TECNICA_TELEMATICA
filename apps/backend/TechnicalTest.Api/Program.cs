using Microsoft.EntityFrameworkCore;
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

// CONFIGURACIÓN DE MYSQL CON ENTITY FRAMEWORK CORE

// Leemos la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Registramos el DbContext con el proveedor
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString, 
        ServerVersion.AutoDetect(connectionString)
    )
);

// Reemplazamos JsonUserRepository por MySqlUserRepository
builder.Services.AddScoped<IUserRepository, MySqlUserRepository>();

// ==============================================================================

builder.Services.AddScoped<SearchUsers.Handler>();
builder.Services.AddScoped<IEmailNotifier, LoggingEmailNotifier>();
builder.Services.AddScoped<CreateUser.Handler>();
builder.Services.AddScoped<UpdateUser.Handler>();

var app = builder.Build();

// CREACIÓN AUTOMÁTICA DE TABLAS EN LA BASE DE DATOS

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated(); 
}
// ==============================================================================

// Habilitar Swagger UI  
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Technical Test API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

UsersGetEndpoint.MapEndpoint(app);
app.MapUsersPostEndpoint();
app.MapUsersPutEndpoint();

app.Run();