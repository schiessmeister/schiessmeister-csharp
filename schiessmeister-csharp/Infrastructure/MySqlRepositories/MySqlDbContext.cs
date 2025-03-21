using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Infrastructure.MySqlRepositories;

public class MySqlDbContext : DbContext {
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Shooter> Shooters { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) {
    }
}