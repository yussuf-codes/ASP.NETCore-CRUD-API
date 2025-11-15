using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Exceptions;
using API.Models;
using API.Repositories.IRepositories;
using API.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class AuthService : IAuthService
{
    private readonly JWTSettings _jwtSettings;
    private readonly IUsersRepository _usersRepository;

    public AuthService(JWTSettings jwtSettings, IUsersRepository usersRepository)
    {
        _jwtSettings = jwtSettings;
        _usersRepository = usersRepository;
    }

    public async Task RegisterAsync(UserRequest request)
    {
        if (await _usersRepository.ExistsAsync(request.Username))
            throw new ConflictException();

        using HMACSHA512 hmac = new HMACSHA512();

        User user = new User()
        {
            Username = request.Username,
            Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            Salt = hmac.Key
        };

        await _usersRepository.CreateAsync(user);
    }

    public async Task<LoginResponse> LoginAsync(UserRequest request)
    {
        if (!await _usersRepository.ExistsAsync(request.Username))
            throw new ConflictException();

        User user = await _usersRepository.GetAsync(request.Username);

        using HMACSHA512 hmac = new HMACSHA512(user.Salt);

        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        if (!hash.SequenceEqual(user.Hash))
            throw new UnauthorizedException();

        LoginResponse response = new LoginResponse()
        {
            Username = user.Username,
            AccessToken = GenerateToken(user.Id, user.Username),
            ExpiresIn = _jwtSettings.Lifetime
        };

        return response;
    }

    private string GenerateToken(Guid userId, string username)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(_jwtSettings.signingKey);
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        {
            new Claim("username", username),
            new Claim("sub", userId.ToString())
        };

        DateTime utcNow = DateTime.UtcNow;

        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Audience = _jwtSettings.Audience,
            Issuer = _jwtSettings.Issuer,
            IssuedAt = utcNow,
            NotBefore = utcNow,
            Expires = utcNow.AddMinutes(_jwtSettings.Lifetime),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        SecurityToken token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }
}
