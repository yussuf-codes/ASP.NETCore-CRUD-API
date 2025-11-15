using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories.IRepositories;

public interface INotesRepository
{
    Task<Note> CreateAsync(Note note);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id, Guid userId);
    Task<IEnumerable<Note>> GetAllAsync(Guid userId);
    Task<Note> GetByIdAsync(Guid id, Guid userId);
    Task UpdateAsync(Guid id, Note note);
}
