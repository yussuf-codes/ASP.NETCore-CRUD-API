using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;

namespace API.Mappings;

public static class NoteMappings
{
    public static GetNoteResponse MapToResponse(this Note note)
    {
        return new GetNoteResponse()
        {
            Id = note.Id,
            Title = note.Title,
            Body = note.Body
        };
    }

    public static Note MapToNote(this CreateNoteRequest request)
    {
        return new Note()
        {
            Title = request.Title,
            Body = request.Body
        };
    }

    public static Note MapToNote(this UpdateNoteRequest request)
    {
        return new Note()
        {
            Id = request.Id,
            Title = request.Title,
            Body = request.Body
        };
    }
}
