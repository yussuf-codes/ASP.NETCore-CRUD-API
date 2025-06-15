using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
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

    public bool Exists(Guid id)
    {
        Note? obj = _dbContext.Notes.SingleOrDefault(obj => obj.Id == id);
        if (obj is null)
            return false;
        return true;
    }

    public IEnumerable<Note> Get() => _dbContext.Notes.AsNoTracking().ToList();

    public Note Get(Guid id) => _dbContext.Notes.AsNoTracking().Single(obj => obj.Id == id);

    public void Update(Guid id, Note obj)
    {
        _dbContext.Notes.Entry(_dbContext.Notes.Single(note => note.Id == id)).CurrentValues.SetValues(obj);
        _dbContext.SaveChanges();
    }
}
