using System;
using System.Collections.Generic;
using API.Models;

namespace API.Repositories.IRepositories;

public interface INotesRepository
{
    Note Create(Note obj);
    void Delete(Guid id);
    bool Exists(Guid id, Guid userId);
    IEnumerable<Note> Get(Guid userId);
    Note Get(Guid id, Guid userId);
    void Update(Guid id, Note obj);
}
