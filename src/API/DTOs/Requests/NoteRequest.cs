namespace API.DTOs.Requests;

public class NoteRequest
{
    public required string Title { get; init; }
    public required string Body { get; init; }
}
