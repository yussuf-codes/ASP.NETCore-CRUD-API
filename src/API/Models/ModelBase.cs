using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public abstract class ModelBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}
