using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Abstractions;

public abstract class ModelBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
