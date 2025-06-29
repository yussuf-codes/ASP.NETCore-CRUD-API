using System;
using API.Models.Abstractions;

namespace API.Models;

public class Note : ModelBase
{
    public required string Title { get; set; }
    public required string Body { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
}
