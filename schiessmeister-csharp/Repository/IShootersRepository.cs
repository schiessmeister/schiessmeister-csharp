using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.Repository {
    public interface IShootersRepository : IRepository<Shooters> {
        public Task<Participation> GetParticipationByShooterAsync(int shooterId);
    }
}
