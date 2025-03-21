using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Domain.Repositories.MySqlRepositories;

public class MySqlDbContext : DbContext
{
    public DbSet<Organizer> Organizers { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) {}
}