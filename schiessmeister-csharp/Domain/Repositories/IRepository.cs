using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Domain.Repositories;

#region Empty repository interfaces

public interface IAppUserRepository : IRepository<AppUser> { }

public interface IDisciplineRepository : IRepository<Discipline> { }

public interface IOrganizationRepository : IRepository<Organization> { }

public interface IParticipationRepository : IRepository<Participation> { }

public interface IParticipationGroupRepository : IRepository<ParticipationGroup> { }

#endregion Empty repository interfaces

public interface IRepository<TEntity> where TEntity : IEntity {

    public Task<List<TEntity>> FindAllAsync();

    public Task<TEntity> AddAsync(TEntity entity);

    public Task<TEntity?> FindByIdAsync(int id);

    public Task<TEntity> UpdateAsync(TEntity entity);

    public Task DeleteAsync(TEntity entity);
}

public interface IEntity {
    public int Id { get; set; }
}