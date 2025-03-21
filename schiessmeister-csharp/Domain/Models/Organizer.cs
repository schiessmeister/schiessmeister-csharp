using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Domain.Models;

public class Organizer : IEntity {
        public int Id { get; set; }
        public string name {get; set;}
        public IEnumerable<Competition>? Competitions {get; set;}
}