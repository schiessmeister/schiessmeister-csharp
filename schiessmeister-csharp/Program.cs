using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Repositories;
using schiessmeister_csharp.Infrastructure.MySqlRepositories;

namespace schiessmeister_csharp;

public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        if (builder.Environment.IsDevelopment()) {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        string? connString = builder.Configuration.GetConnectionString("timon.mysql"); // Change according to current machine.
        if (connString == null) return;

        builder.Services.AddDbContext<MySqlDbContext>(options => options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

        builder.Services.AddScoped<IOrganizerRepository, MySqlOrganizerRepository>();
        builder.Services.AddScoped<ICompetitionRepository, MySqlCompetitionRepository>();
        builder.Services.AddScoped<IShooterRepository, MySqlShooterRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}