using System;
using System.Collections.Generic;
using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services.IServices;

public interface INotesService
{
    Guid Create(CreateNoteRequest request);
    void Delete(Guid id);
    GetNoteResponse Get(Guid id);
    IEnumerable<GetNoteResponse> Get();
    void Update(Guid id, UpdateNoteRequest request);
}
