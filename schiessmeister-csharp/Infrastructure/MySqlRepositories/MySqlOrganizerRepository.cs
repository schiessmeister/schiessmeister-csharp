using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Infrastructure.MySqlRepositories;

public class MySqlOrganizerRepository : IOrganizerRepository {
    private readonly MySqlDbContext _db;

    public MySqlOrganizerRepository(MySqlDbContext dbContext) {
        _db = dbContext;
    }

    public async Task<List<Organizer>> FindAllAsync() {
        return await _db.Organizers.ToListAsync();
    }

    public async Task<Organizer?> FindByIdAsync(int id) {
        return await _db.Organizers.FindAsync(id);
    }

    public async Task<Organizer> AddAsync(Organizer entity) {
        await _db.Organizers.AddAsync(entity);

        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Organizer> UpdateAsync(Organizer entity) {
        var existing = await _db.Organizers.FindAsync(entity.Id);

        if (existing == null)
            throw new InvalidOperationException("Organizer does not exist");

        _db.Entry(existing).CurrentValues.SetValues(entity);
        _db.Organizers.Update(existing);

        await _db.SaveChangesAsync();

        return existing;
    }

    public async Task DeleteAsync(Organizer entity) {
        var existing = await _db.Organizers.FindAsync(entity.Id);

        if (existing == null)
            throw new InvalidOperationException("Organizer does not exist");

        _db.Organizers.Remove(existing);

        await _db.SaveChangesAsync();
    }
}