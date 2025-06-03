using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Domain.Repositories;

public interface IParticipationGroupRepository : IRepository<ParticipationGroup> {

    public Task<ParticipationGroup?> FindByIdWithAllParentsCompAsync(int id);

    public Task<ParticipationGroup?> FindByIdWithAllParentsChildsCompAsync(int id);
}