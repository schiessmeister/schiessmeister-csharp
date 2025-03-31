using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using schiessmeister_csharp.Infrastructure.MySqlRepositories;

namespace schiessmeister_csharp.Infrastructure;

public class SeedDB {

    public static async void Initialize(IServiceProvider serviceProvider) {
        var context = serviceProvider.GetRequiredService<MySqlDbContext>();

        string[] roles = [
            "User",
            "Organizer"
        ];

        RoleStore<IdentityRole> roleStore = new(context);

        foreach (string role in roles) {
            if (!context.Roles.Any(r => r.Name == role)) {
                await roleStore.CreateAsync(new IdentityRole() {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
        }
    }
}