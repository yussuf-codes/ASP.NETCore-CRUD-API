using System;
using System.Collections.Generic;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Exceptions;
using API.Mappings;
using API.Models;
using API.Repositories.IRepositories;
using API.Services.IServices;

namespace API.Services;

public class NotesService : INotesService
{
    private readonly INotesRepository _repository;

    public NotesService(INotesRepository repository)
    {
        _repository = repository;
    }

    public GetNoteResponse Create(NoteRequest request, Guid userId)
    {
        Note note = request.MapToNote();
        note.UserId = userId;
        note = _repository.Create(note);
        GetNoteResponse response = note.MapToResponse();
        return response;
    }

    public void Delete(Guid id, Guid userId)
    {
        if (!_repository.Exists(id, userId))
            throw new NotFoundException();
        _repository.Delete(id);
    }

    public IEnumerable<GetNoteResponse> Get(Guid userId)
    {
        IEnumerable<Note> notes = _repository.Get(userId);
        List<GetNoteResponse> response = new List<GetNoteResponse>();
        foreach (Note note in notes)
            response.Add(note.MapToResponse());
        return response;
    }

    public GetNoteResponse Get(Guid id, Guid userId)
    {
        if (!_repository.Exists(id, userId))
            throw new NotFoundException();
        Note note = _repository.Get(id, userId);
        GetNoteResponse response = note.MapToResponse();
        return response;
    }

    public void Update(Guid id, Guid userId, NoteRequest request)
    {
        throw new NotImplementedException();
    }
}
