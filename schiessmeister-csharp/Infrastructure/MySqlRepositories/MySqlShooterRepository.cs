using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Infrastructure.MySqlRepositories;

public class MySqlShooterRepository : IShooterRepository {
    private readonly MySqlDbContext _db;

    public MySqlShooterRepository(MySqlDbContext dbContext) {
        _db = dbContext;
    }

    public async Task<List<Shooter>> FindAllAsync() {
        return await _db.Shooters.ToListAsync();
    }

    public async Task<Shooter?> FindByIdAsync(int id) {
        return await _db.Shooters.FindAsync(id);
    }

    public async Task<Shooter> AddAsync(Shooter entity) {
        await _db.Shooters.AddAsync(entity);

        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Shooter> UpdateAsync(Shooter entity) {
        var existing = await _db.Shooters.FindAsync(entity.Id);

        if (existing == null)
            throw new InvalidOperationException("Shooter does not exist");

        _db.Entry(existing).CurrentValues.SetValues(entity);
        _db.Shooters.Update(existing);

        await _db.SaveChangesAsync();

        return existing;
    }

    public async Task DeleteAsync(Shooter entity) {
        var existing = await _db.Shooters.FindAsync(entity.Id);

        if (existing == null)
            throw new InvalidOperationException("Shooter does not exist");

        _db.Shooters.Remove(existing);

        await _db.SaveChangesAsync();
    }
}