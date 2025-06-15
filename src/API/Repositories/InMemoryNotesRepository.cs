using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Repositories.IRepositories;

namespace API.Repositories;

public class InMemoryNotesRepository : INotesRepository
{
    private readonly List<Note> _notes = new()
    {
        new Note() { Title = "Shopping", Body = "Buy some coffee" },
        new Note() { Title = "SOLID Principles", Body = "SIP, OCP, LSP, ISP, DIP" }
    };

    private int GetIndex(Guid id) => _notes.FindIndex(obj => obj.Id == id);
    public Note Create(Note obj)
    {
        _notes.Add(obj);
        return _notes[GetIndex(obj.Id)];
    }
    public void Delete(Guid id) => _notes.RemoveAt(GetIndex(id));
    public bool Exists(Guid id) => _notes.Any(obj => obj.Id == id);
    public IEnumerable<Note> Get() => _notes;
    public Note Get(Guid id) => _notes[GetIndex(id)];
    public void Update(Guid id, Note obj) => _notes[GetIndex(id)] = obj;
}
