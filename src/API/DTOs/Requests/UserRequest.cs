using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests;

public class UserRequest
{
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
    [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
    public required string Username { get; init; }
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public required string Password { get; init; }
}
