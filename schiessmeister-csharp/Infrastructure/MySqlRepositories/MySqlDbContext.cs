using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Identity;


namespace schiessmeister_csharp.Domain.Repositories.MySqlRepositories;

public class MySqlDbContext: IdentityDbContext<ApplicationUser>
{
    public DbSet<Organizer> Organizers { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) {
       
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}