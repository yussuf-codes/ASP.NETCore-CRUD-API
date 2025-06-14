using System;

namespace API.Models;

public abstract class ModelBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
