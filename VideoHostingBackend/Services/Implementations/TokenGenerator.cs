using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Core.Services;

namespace VideoHostingBackend.Services.Implementations;

internal class TokenGenerator : ITokenGenerator
{
    public Task<string> GenerateToken(UserCredentials credentials)
    {
        List<Claim> claims = new()
        {
            new("Id", credentials.UserId.ToString())
        };

        JwtSecurityToken token = new(
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.Add(AuthenticationOptions.Lifetime),
            claims: claims,
            signingCredentials: new(AuthenticationOptions.GetSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        JwtSecurityTokenHandler handler = new();
        return Task.FromResult(handler.WriteToken(token));
    }
}