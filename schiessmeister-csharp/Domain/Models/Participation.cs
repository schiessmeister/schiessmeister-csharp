using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class Participation : IEntity {
    public int Id { get; set; }
    public string Class { get; set; }
    public string Results { get; set; }
    public int OrderNb { get; set; }

    public int ShooterId { get; set; }
    public Shooter Shooter { get; set; } = null!;
    public int CompetitionId { get; set; }
    public Competition Competition { get; set; } = null!;
}