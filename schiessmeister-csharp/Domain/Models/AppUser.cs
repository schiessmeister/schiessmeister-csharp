using Microsoft.AspNetCore.Identity;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class AppUser : IdentityUser<int>, IEntity {
    public OrganizerProfile? OrganizerProfile { get; set; }
}

public class OrganizerProfile {
    public List<Competition> Competitions { get; set; } = [];
}