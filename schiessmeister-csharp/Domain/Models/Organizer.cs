using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class Organizer : IEntity {
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Competition> Competitions { get; set; } = [];
}