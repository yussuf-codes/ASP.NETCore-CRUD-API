using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Persistence;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EFCoreNotesRepository : INotesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EFCoreNotesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Note> CreateAsync(Note obj)
    {
        await _dbContext.Notes.AddAsync(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }

    public async Task DeleteAsync(Guid id)
    {
        Note obj = await _dbContext.Notes.SingleAsync(obj => obj.Id == id);
        _dbContext.Notes.Remove(obj);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id, Guid userId)
    {
        return await _dbContext.Notes
                            .AsNoTracking()
                            .AnyAsync(obj => obj.Id == id && obj.UserId == userId);
    }

    public async Task<IEnumerable<Note>> GetAllAsync(Guid userId)
    {
        return await _dbContext.Notes
                            .AsNoTracking()
                            .Where(n => n.UserId == userId)
                            .ToListAsync();
    }

    public async Task<Note> GetByIdAsync(Guid id, Guid userId)
    {
        return await _dbContext.Notes
                            .AsNoTracking()
                            .Where(n => n.UserId == userId)
                            .SingleAsync(obj => obj.Id == id);
    }

    public async Task UpdateAsync(Guid id, Note obj)
    {
        Note existing = await _dbContext.Notes.SingleAsync(n => n.Id == id);

        existing.Title = obj.Title;
        existing.Body = obj.Body;

        await _dbContext.SaveChangesAsync();
    }
}
