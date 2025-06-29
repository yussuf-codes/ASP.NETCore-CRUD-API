using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Abstractions;

public abstract class ModelBase
{
    [Key]
    public Guid Id { get; } = Guid.NewGuid();
}
