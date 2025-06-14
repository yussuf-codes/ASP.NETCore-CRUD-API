using System;

namespace API.DTOs.Responses;

public class GetNoteResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}
