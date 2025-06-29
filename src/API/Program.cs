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
        string AllowAllOrigins = "AllowAllOrigins";

        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

        JWTConfig jwtConfig = builder.Configuration.GetSection("JWTConfig").Get<JWTConfig>()!;

        builder.Services.AddSingleton(jwtConfig);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
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

        builder.Services.AddOpenApi();

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

        // string? connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
        // builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

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
