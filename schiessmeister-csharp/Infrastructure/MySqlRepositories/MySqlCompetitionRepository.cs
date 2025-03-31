using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;
using System.Runtime.InteropServices;

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

    public async Task<Competition?> FindByIdFullAsync(int id) {
        return await _db.Competitions
            .Include(c => c.Participations.OrderBy(p => p.OrderNb))
            .ThenInclude(p => p.Shooter)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Competition>> FindByOrganizerIdAsync(int organizerId) {
        return await _db.Competitions.Where(c => c.OrganizerId == organizerId).ToListAsync();
    }

    public async Task<Competition> AddAsync(Competition entity) {
        await _db.Competitions.AddAsync(entity);

        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Competition> UpdateAsync(Competition entity) {
        var existing = await _db.Competitions
            .Include(c => c.Participations)
            .FirstOrDefaultAsync(c => c.Id == entity.Id);

        if (existing == null)
            throw new InvalidOperationException("Competition does not exist");

        // Update competition properties
        _db.Entry(existing).CurrentValues.SetValues(entity);

        // Remove all existing participations
        if (existing.Participations != null && existing.Participations.Any()) {
            _db.Participations.RemoveRange(existing.Participations);
        }

        // Add new participations from the entity
        if (entity.Participations != null && entity.Participations.Any()) {
            foreach (var participation in entity.Participations) {
                participation.CompetitionId = existing.Id;
                await _db.Participations.AddAsync(participation);
            }
        }

        await _db.SaveChangesAsync();

        return existing;
    }

    public async Task DeleteAsync(Competition entity) {
        var existing = await _db.Competitions
            .Include(c => c.Participations)
            .FirstOrDefaultAsync(c => c.Id == entity.Id);

        if (existing == null)
            throw new InvalidOperationException("Competition does not exist");

        if (existing.Participations != null && existing.Participations.Any()) {
            _db.Participations.RemoveRange(existing.Participations);
        }

        _db.Competitions.Remove(existing);

        await _db.SaveChangesAsync();
    }
}