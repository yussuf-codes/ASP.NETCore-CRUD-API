using System;
using System.Collections.Generic;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Exceptions;
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

    public GetNoteResponse Create(CreateNoteRequest request)
    {
        Note note = request.MapToNote();
        GetNoteResponse response = _repository.Create(note).MapToResponse();
        return response;
    }

    public void Delete(Guid id)
    {
        if (!_repository.Exists(id))
            throw new NotFoundException();
        _repository.Delete(id);
    }

    public IEnumerable<GetNoteResponse> Get()
    {
        IEnumerable<Note> notes = _repository.Get();
        List<GetNoteResponse> response = new List<GetNoteResponse>();
        foreach (Note note in notes)
            response.Add(note.MapToResponse());
        return response;
    }

    public GetNoteResponse Get(Guid id)
    {
        if (!_repository.Exists(id))
            throw new NotFoundException();
        Note note = _repository.Get(id);
        GetNoteResponse response = note.MapToResponse();
        return response;
    }

    public void Update(Guid id, UpdateNoteRequest request)
    {
        if (id != request.Id)
            throw new BadRequestException();
        if (!_repository.Exists(id))
            throw new NotFoundException();
        Note note = request.MapToNote();
        note.Id = request.Id;
        _repository.Update(id, note);
    }
}
