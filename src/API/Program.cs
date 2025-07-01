using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Persistence;
using API.Repositories;
using API.Repositories.IRepositories;
using API.Services;
using API.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace API;

public static class Program
{
    public static async Task Main()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        Assembly executingAssembly = Assembly.GetExecutingAssembly();

        builder.Configuration.AddUserSecrets(executingAssembly);

        JWTSettings jwtSettings = new JWTSettings()
        {
            signingKey = Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SigningKey"] ?? "TotallyInsecureJwtKeyButGreatForDebugging")
        };

        builder.Services.AddSingleton(jwtSettings);

        builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = jwtSettings.Audience,
                        ValidIssuer = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.signingKey),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                });

        builder.Services.AddAuthorization();

        builder.Services.AddControllers();

        builder.Services.AddScoped<INotesRepository, EFCoreNotesRepository>();
        builder.Services.AddScoped<IUsersRepository, EFCoreUsersRepository>();

        builder.Services.AddScoped<INotesService, NotesService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        string AllowAllOrigins = "AllowAllOrigins";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: AllowAllOrigins, policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin();
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
            });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("Database"));

        // string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"] ?? throw new Exception("Connection string is not provided.");

        // builder.Services.AddDbContext<ApplicationDbContext>
        // (
        //     options => options.UseSqlServer(connectionString)
        // );

        builder.Services.AddOpenApi();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        if (app.Environment.IsProduction())
            app.UseHttpsRedirection();

        app.UseCors(AllowAllOrigins);

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}
