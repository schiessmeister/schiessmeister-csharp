using Microsoft.AspNetCore.Identity;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class AppUser : IdentityUser<int>, IEntity {
    public List<Competition>? Competitions { get; set; } = [];
}