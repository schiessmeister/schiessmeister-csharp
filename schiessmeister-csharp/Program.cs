using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Repositories;
using schiessmeister_csharp.Domain.Repositories.MySqlRepositories;
using schiessmeister_csharp.Infrastructure.MySqlRepositories;

namespace schiessmeister_csharp;

public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        // Add services to the container.
        builder.Services.AddControllers();

        
        var connectionString = builder.Configuration.GetConnectionString("mySqlDb");
        builder.Services.AddDbContext<MySqlDbContext>(options => 
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))        
        );
        
        
        builder.Services.AddScoped<IOrganizerRepository, MySqlOrganizerRepository>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
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