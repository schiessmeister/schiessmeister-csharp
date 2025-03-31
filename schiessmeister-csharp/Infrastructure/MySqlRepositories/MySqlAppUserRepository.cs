using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Infrastructure.MySqlRepositories;

public class MySqlAppUserRepository : IAppUserRepository {
    private readonly MySqlDbContext _db;

    public MySqlAppUserRepository(MySqlDbContext dbContext) {
        _db = dbContext;
    }

    public async Task<List<AppUser>> FindAllAsync() {
        return await _db.AppUsers.ToListAsync();
    }

    public async Task<AppUser?> FindByIdAsync(int id) {
        return await _db.AppUsers.FindAsync(id);
    }

    public async Task<AppUser> AddAsync(AppUser entity) {
        await _db.AppUsers.AddAsync(entity);

        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<AppUser> UpdateAsync(AppUser entity) {
        var existing = await _db.AppUsers.FindAsync(entity.Id);

        if (existing == null)
            throw new InvalidOperationException("User does not exist");

        _db.Entry(existing).CurrentValues.SetValues(entity);
        _db.AppUsers.Update(existing);

        await _db.SaveChangesAsync();

        return existing;
    }

    public async Task DeleteAsync(AppUser entity) {
        var existing = await _db.AppUsers.FindAsync(entity.Id);

        if (existing == null)
            throw new InvalidOperationException("User does not exist");

        _db.AppUsers.Remove(existing);

        await _db.SaveChangesAsync();
    }
}