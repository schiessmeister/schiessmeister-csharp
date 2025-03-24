using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class Shooter : IEntity {
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Participation> Participations { get; set; } = [];
}