using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using schiessmeister_csharp.Infrastructure.MySqlRepositories;

namespace schiessmeister_csharp.Infrastructure;

public class SeedDB {

    public static async void Initialize(IServiceProvider serviceProvider) {
        var context = serviceProvider.GetRequiredService<MySqlDbContext>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        string[] roles = {
            "User",
            "Organizer"
        };

        foreach (string role in roles) {
            if (!context.Roles.Any(r => r.Name == role)) {
                await roleManager.CreateAsync(new IdentityRole<int>() {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
        }
    }
}