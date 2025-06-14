namespace API.DTOs.Requests;

public class CreateNoteRequest
{
    public required string Title { get; init; }
    public required string Body { get; init; }
}
