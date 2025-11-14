using System;
using System.Collections.Generic;
using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services.IServices;

public interface INotesService
{
    GetNoteResponse Create(NoteRequest request, Guid userId);
    void Delete(Guid id, Guid userId);
    GetNoteResponse Get(Guid id, Guid userId);
    IEnumerable<GetNoteResponse> Get(Guid userId);
    void Update(Guid id, Guid userId, NoteRequest request);
}
