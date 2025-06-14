using System;
using System.Collections.Generic;
using API.Models;

namespace API.Repositories.IRepositories;

public interface INotesRepository
{
    void Add(Note obj);
    void Delete(Guid id);
    bool Exists(Guid id);
    IEnumerable<Note> Get();
    Note Get(Guid id);
    void Update(Guid id, Note obj);
}
