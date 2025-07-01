namespace API.Models;

public class JWTSettings
{
    public string Audience { get; } = "<audience>";
    public string Issuer { get; } = "<issuer>";
    public int Lifetime { get; } = 30;
    public required byte[] signingKey { get; init; }
}
