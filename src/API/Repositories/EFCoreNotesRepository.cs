using System;
using System.Collections.Generic;
using System.Linq;
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

    public Note Create(Note obj)
    {
        _dbContext.Notes.Add(obj);
        _dbContext.SaveChanges();
        return obj;
    }

    public void Delete(Guid id)
    {
        Note obj = _dbContext.Notes.Single(obj => obj.Id == id);
        _dbContext.Notes.Remove(obj);
        _dbContext.SaveChanges();
    }

    public bool Exists(Guid id, Guid userId)
    {
        Note? obj = _dbContext.Notes
                            .AsNoTracking()
                            .Where(n => n.UserId == userId)
                            .SingleOrDefault(obj => obj.Id == id);

        if (obj is null)
            return false;
        return true;
    }

    public IEnumerable<Note> Get(Guid userId)
    {
        return _dbContext.Notes
                            .AsNoTracking()
                            .Where(n => n.UserId == userId)
                            .ToList();
    }

    public Note Get(Guid id, Guid userId)
    {
        return _dbContext.Notes
                            .AsNoTracking()
                            .Where(n => n.UserId == userId)
                            .Single(obj => obj.Id == id);
    }

    public void Update(Guid id, Note obj)
    {
        Note existing = _dbContext.Notes.Single(n => n.Id == id);

        existing.Title = obj.Title;
        existing.Body = obj.Body;

        _dbContext.SaveChanges();
    }
}
