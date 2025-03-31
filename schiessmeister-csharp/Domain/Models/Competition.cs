using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class Competition : IEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }

    public int OrganizerId { get; set; }
    public AppUser Organizer { get; set; } = null!;

    public List<Participation> Participations { get; set; } = [];
}