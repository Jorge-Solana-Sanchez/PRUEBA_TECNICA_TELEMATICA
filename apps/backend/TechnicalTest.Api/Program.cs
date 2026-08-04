using Microsoft.EntityFrameworkCore;
using TechnicalTest.Api.Endpoints.Users;
using TechnicalTest.User.Users.Application.Creator;
using TechnicalTest.User.Users.Application.Finder;
using TechnicalTest.User.Users.Application.Notifier;
using TechnicalTest.User.Users.Application.Updater;
using TechnicalTest.User.Users.Domain;
using TechnicalTest.User.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString, 
        ServerVersion.AutoDetect(connectionString)
    )
);

builder.Services.AddScoped<IUserRepository, MySqlUserRepository>();
builder.Services.AddScoped<IEmailNotifier, LoggingEmailNotifier>();

builder.Services.AddScoped<SearchUsers.Handler>();
builder.Services.AddScoped<CreateUser.Handler>();
builder.Services.AddScoped<UpdateUser.Handler>();

var app = builder.Build();

// Ensure DB schema is created on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated(); 
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Technical Test API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseCors();

UsersGetEndpoint.MapEndpoint(app);
app.MapUsersPostEndpoint();
app.MapUsersPutEndpoint();

app.Run();