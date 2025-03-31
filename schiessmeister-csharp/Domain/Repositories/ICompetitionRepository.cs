using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Domain.Repositories;

public interface ICompetitionRepository : IRepository<Competition> {

    public Task<Competition?> FindByIdFullAsync(int id);

    public Task<List<Competition>> FindByOrganizerIdAsync(int organizerId);
}