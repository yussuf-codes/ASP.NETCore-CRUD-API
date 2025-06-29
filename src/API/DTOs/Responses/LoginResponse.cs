namespace API.DTOs.Responses;

public class LoginResponse
{
    public required string Username { get; init; }
    public required string AccessToken { get; set; }
    public required int ExpiresIn { get; set; }
}
