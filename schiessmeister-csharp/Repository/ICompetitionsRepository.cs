using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Repository {
    public interface ICompetitionsRepository : IRepository<Competitions> {
        Task<List<Participation>> GetParticipationsByCompetitionAsync(int competitionId);
    }
}
