using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
    private readonly JWTConfig _jwtConfig;
    private readonly IUsersRepository _usersRepository;

    public AuthService(IUsersRepository usersRepository, JWTConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;
        _usersRepository = usersRepository;
    }

    public void Register(UserRequest request)
    {
        if (_usersRepository.Exists(request.Username))
            throw new ConflictException();

        using HMACSHA512 hmac = new HMACSHA512();

        User user = new User()
        {
            Username = request.Username,
            Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            Salt = hmac.Key
        };

        _usersRepository.Create(user);
    }

    public LoginResponse Login(UserRequest request)
    {
        User? user = _usersRepository.Get(request.Username);

        if (user is null)
            throw new NotFoundException();

        using HMACSHA512 hmac = new HMACSHA512(user.Salt);

        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        if (!hash.SequenceEqual(user.Hash))
            throw new UnauthorizedException();

        LoginResponse response = new LoginResponse()
        {
            Username = user.Username,
            AccessToken = GenerateToken(user.Id, user.Username),
            ExpiresIn = _jwtConfig.Lifetime
        };

        return response;
    }

    private string GenerateToken(Guid userId, string username)
    {
        byte[] key_bytes = Encoding.UTF8.GetBytes(_jwtConfig.Key);
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key_bytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.Lifetime),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        SecurityToken token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }
}
