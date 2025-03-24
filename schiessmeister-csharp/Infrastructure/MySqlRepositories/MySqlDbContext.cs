using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Identity;

namespace schiessmeister_csharp.Infrastructure.MySqlRepositories;

public class MySqlDbContext : IdentityDbContext<ApplicationUser> {
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Shooter> Shooters { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) {
    }
}