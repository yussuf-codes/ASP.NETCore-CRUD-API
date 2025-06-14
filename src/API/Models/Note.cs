namespace API.Models;

public class Note : ModelBase
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}
