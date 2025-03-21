using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Repositories.MySqlRepositories;
using schiessmeister_csharp.Identity;

namespace schiessmeister_csharp.Infrastructure;

public class SeedDB
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<MySqlDbContext>();

        string[] roles = new string[] { "User" };

        foreach (string role in roles)
        {
            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == role))
            {
                await roleStore.CreateAsync(new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
        }
    }


}