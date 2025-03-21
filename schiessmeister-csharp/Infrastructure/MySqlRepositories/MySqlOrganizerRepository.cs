using Microsoft.EntityFrameworkCore;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;
using schiessmeister_csharp.Domain.Repositories.MySqlRepositories;

namespace schiessmeister_csharp.Infrastructure.MySqlRepositories;

public class MySqlOrganizerRepository : IOrganizerRepository {
    
    private readonly MySqlDbContext _context;
    
    public MySqlOrganizerRepository(MySqlDbContext mysqlDbContext)
    {
        _context = mysqlDbContext;
    }
    
    public async Task<List<Organizer>> FindAllAsync() {
        return await _context.Organizers.ToListAsync();
    }

    public async Task<Organizer> AddAsync(Organizer entity) {
        await _context.Organizers.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Organizer?> FindByIdAsync(int id) {
        return await _context.Organizers.FindAsync(id);
    }

    public async Task<Organizer> UpdateAsync(Organizer entity) {
        var existingEntity = await _context.Organizers.FindAsync(entity.Id);
        if(existingEntity==null)
            throw new ArgumentException("Organizer doesn't exist");
        
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        
        _context.Organizers.Update(entity);
        
        await _context.SaveChangesAsync();
        
        return existingEntity;
    }

    public async Task DeleteAsync(Organizer entity) {

        var existing = await _context.Organizers.FindAsync(entity.Id);
        
        if (existing == null)
            throw new ArgumentException("Organizer not found");
        
        _context.Organizers.Remove(entity);

        await _context.SaveChangesAsync();
    }
}