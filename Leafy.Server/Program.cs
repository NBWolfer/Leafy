using Leafy.Application.Interfaces;
using Leafy.Application.Services;
using Leafy.Persistance.Context;
using Leafy.Persistance.Repositories;
using Leafy.Server.Extensions;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<LeafyContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddApplicationServices(builder.Configuration);


builder.Services.AddControllers();

builder.Services.AddAuthentication("Cookie-0")
    .AddCookie("Cookie-0", options =>
    {
        options.Cookie.Name = "Cookie-0";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
    options.AddPolicy("user", policy => policy.RequireClaim(ClaimTypes.Role, "user"));
    options.AddPolicy("admin-user", policy => policy.RequireRole("user","admin"));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
