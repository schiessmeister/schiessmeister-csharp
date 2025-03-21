using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Infrastructure.MySqlRepositories;

public class MySqlCompetitionRepository : ICompetitionRepository {
    private readonly MySqlDbContext _db;

    public MySqlCompetitionRepository(MySqlDbContext dbContext) {
        _db = dbContext;
    }

    public async Task<List<Competition>> FindAllAsync() {
        return await _db.Competitions.ToListAsync();
    }

    public async Task<Competition?> FindByIdAsync(int id) {
        return await _db.Competitions.FindAsync(id);
    }

    public async Task<Competition> AddAsync(Competition entity) {
        await _db.Competitions.AddAsync(entity);

        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Competition> UpdateAsync(Competition entity) {
        var existing = await _db.Competitions.FindAsync(entity.Id);

        if (existing == null)
            throw new InvalidOperationException("Competition does not exist");

        _db.Entry(existing).CurrentValues.SetValues(entity);
        _db.Competitions.Update(existing);

        await _db.SaveChangesAsync();

        return existing;
    }

    public async Task DeleteAsync(Competition entity) {
        var existing = await _db.Competitions.FindAsync(entity.Id);

        if (existing == null)
            throw new InvalidOperationException("Competition does not exist");

        _db.Competitions.Remove(existing);

        await _db.SaveChangesAsync();
    }
}