namespace API.Models;

public class JWTConfig
{
    public required string Audience { get; set; }
    public required string Issuer { get; set; }
    public required string Key { get; set; }
    public required int Lifetime { get; set; }
}
