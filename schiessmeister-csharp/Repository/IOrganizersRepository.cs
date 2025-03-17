using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Repository {
    public interface IOrganizersRepository : IRepository<Organizers> {
        public Task<List<Competitions>> GetCompetitionsByOrganizerAsync(int organizerId);
    }
}
