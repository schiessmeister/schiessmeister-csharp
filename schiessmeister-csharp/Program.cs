using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using schiessmeister_csharp.API.Services;
using schiessmeister_csharp.Domain.Repositories;
using schiessmeister_csharp.Domain.Repositories.MySqlRepositories;
using schiessmeister_csharp.Identity;
using schiessmeister_csharp.Infrastructure;
using schiessmeister_csharp.Infrastructure.MySqlRepositories;

namespace schiessmeister_csharp;

public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("mySqlDb");
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "OpenCorsPolicy",
                policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
        });
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddControllers();

      
        builder.Services.AddDbContext<MySqlDbContext>(options => 
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))        
        );
        
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
            { options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false; }).AddEntityFrameworkStores<MySqlDbContext>();
        
        builder.Services.AddAuthentication(a => 
        {
            a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters​
            {
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
        
        builder.Services.AddSwaggerGen(opt =>
        {
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

        
        builder.Services.AddScoped<IOrganizerRepository, MySqlOrganizerRepository>();
        builder.Services.AddScoped<ITokenService, JwtService>();

        var app = builder.Build();

        SeedDB.Initialize( app.Services.GetRequiredService<IServiceProvider>().CreateScope().ServiceProvider );

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        app.Run();
    }
}