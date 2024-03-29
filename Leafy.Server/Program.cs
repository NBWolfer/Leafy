using Leafy.Application.Interfaces;
using Leafy.Application.Services;
using Leafy.Domain.Entities;
using Leafy.Persistance.Context;
using Leafy.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<LeafyContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IToken, Token>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserPlantRepository, UserPlantRepository>();
builder.Services.AddScoped<IDiseaseRepository, DiseaseRepository>();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = builder.Configuration.GetValue<string>("Email-Api-Key");
});

builder.Services.AddControllers();

builder.Services.AddAuthentication("accessToken").AddCookie("accessToken", options =>
{
    options.Cookie.Name = "accessToken";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
}).AddCookie("refreshToken", options =>
{
    options.Cookie.Name = "refreshToken";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("adminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "admin"))
    .AddPolicy("user", policy => policy.RequireClaim(ClaimTypes.Role, "user"))
    .AddPolicy("admin-user", policy => policy.RequireRole("user","admin"));




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Leafy.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat  = "JWT",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
