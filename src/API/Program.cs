using System.Reflection;
using System.Threading.Tasks;
using API.Repositories;
using API.Repositories.IRepositories;
using API.Services;
using API.Services.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

namespace API;

public static class Program
{
    public static async Task Main()
    {
        string AllowAllOrigins = "AllowAllOrigins";

        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

        builder.Services.AddControllers();

        // builder.Services.AddScoped<INotesRepository, EFCoreNotesRepository>();
        builder.Services.AddSingleton<INotesRepository, InMemoryNotesRepository>();
        builder.Services.AddScoped<INotesService, NotesService>();

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

        app.MapControllers();

        await app.RunAsync();
    }
}
