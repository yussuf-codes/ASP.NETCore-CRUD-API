using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services.IServices;

public interface INotesService
{
    Task<GetNoteResponse> CreateAsync(NoteRequest request, Guid userId);
    Task DeleteAsync(Guid id, Guid userId);
    Task<GetNoteResponse> GetAsync(Guid id, Guid userId);
    Task<IEnumerable<GetNoteResponse>> GetAsync(Guid userId);
    Task UpdateAsync(Guid id, NoteRequest request, Guid userId);
}
