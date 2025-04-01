using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using schiessmeister_csharp.API.Services;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;
using schiessmeister_csharp.Infrastructure;
using schiessmeister_csharp.Infrastructure.MySqlRepositories;
using schiessmeister_csharp.API.Hubs;

namespace schiessmeister_csharp;

public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        #region Auth

        builder.Services.AddCors(options => {
            options.AddPolicy(name: "OpenCorsPolicy",
                policy => {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
        });

        builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options => {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        }).AddEntityFrameworkStores<MySqlDbContext>();

        builder.Services.AddAuthentication(a => {
            a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt => {
            opt.TokenValidationParameters = new TokenValidationParameters {
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(
                        builder.Configuration.GetSection("JwtSettings")["Secret"]!
                    )),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        });

        builder.Services.AddScoped<ITokenService, JwtService>();

        #endregion Auth

        if (builder.Environment.IsDevelopment()) {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt => {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new string[] {}
                }
            });
            });
        }

        string? connString = builder.Configuration.GetConnectionString("timon.mysql"); // Change according to current machine.
        if (connString == null) return;

        builder.Services.AddDbContext<MySqlDbContext>(options => options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

        builder.Services.AddScoped<IAppUserRepository, MySqlAppUserRepository>();
        builder.Services.AddScoped<ICompetitionRepository, MySqlCompetitionRepository>();
        builder.Services.AddScoped<IShooterRepository, MySqlShooterRepository>();
        builder.Services.AddSignalR();
        builder.Services.AddScoped<ICompetitionNotificationService, CompetitionNotificationService>();

        var app = builder.Build();

        SeedDB.Initialize(app.Services.GetRequiredService<IServiceProvider>().CreateScope().ServiceProvider);

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("OpenCorsPolicy");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapHub<CompetitionHub>("/hubs/competition");
        app.Run();
    }
}