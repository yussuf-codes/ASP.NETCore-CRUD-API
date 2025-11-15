using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task<GetNoteResponse> CreateAsync(NoteRequest request, Guid userId)
    {
        Note note = request.MapToNote();
        note.UserId = userId;
        note = await _repository.CreateAsync(note);
        GetNoteResponse response = note.MapToResponse();
        return response;
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        if (!await _repository.ExistsAsync(id, userId))
            throw new NotFoundException();
        await _repository.DeleteAsync(id);
    }

    public async Task<GetNoteResponse> GetAsync(Guid id, Guid userId)
    {
        if (!await _repository.ExistsAsync(id, userId))
            throw new NotFoundException();
        Note note = await _repository.GetByIdAsync(id, userId);
        GetNoteResponse response = note.MapToResponse();
        return response;
    }

    public async Task<IEnumerable<GetNoteResponse>> GetAsync(Guid userId)
    {
        IEnumerable<Note> notes = await _repository.GetAllAsync(userId);
        List<GetNoteResponse> response = new List<GetNoteResponse>();
        foreach (Note note in notes)
            response.Add(note.MapToResponse());
        return response;
    }

    public async Task UpdateAsync(Guid id, NoteRequest request, Guid userId)
    {
        if (!await _repository.ExistsAsync(id, userId))
            throw new NotFoundException();
        Note note = request.MapToNote();
        await _repository.UpdateAsync(id, note);
    }
}
