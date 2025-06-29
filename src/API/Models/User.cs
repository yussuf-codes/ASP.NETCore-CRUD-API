using System.Collections.Generic;
using API.Models.Abstractions;

namespace API.Models;

public class User : ModelBase
{
    public required string Username { get; set; }
    public required byte[] Hash { get; set; }
    public required byte[] Salt { get; set; }

    public ICollection<Note>? Notes { get; set; }
}
