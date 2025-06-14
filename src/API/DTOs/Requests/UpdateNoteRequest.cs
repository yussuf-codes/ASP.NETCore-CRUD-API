using System;

namespace API.DTOs.Requests;

public class UpdateNoteRequest
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Body { get; init; }
}
