using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Infrastructure.MySqlRepositories;

public class ParticipationGroupRepository(MySqlDbContext dbContext) : MySqlRepositoryBase<ParticipationGroup>(dbContext, dbContext.ParticipationGroups), IParticipationGroupRepository {

    public async Task<ParticipationGroup?> FindByIdWithAllParentsCompAsync(int id) {
        return await _db.ParticipationGroups
            .Include(pg => pg.ParentGroup)
            .ThenInclude(pg => pg!.Competition) // TODO Check if this works
            .FirstOrDefaultAsync(pg => pg.Id == id);
    }

    public async Task<ParticipationGroup?> FindByIdWithAllParentsChildsCompAsync(int id) {
        return await _db.ParticipationGroups
            .Include(pg => pg.ParentGroup)
            .ThenInclude(pg => pg!.Competition)
            .Include(pg => pg.SubGroups)
            .FirstOrDefaultAsync(pg => pg.Id == id);
    }
}