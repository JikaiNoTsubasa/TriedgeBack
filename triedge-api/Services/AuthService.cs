using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using triedge_api.Database.Models;

namespace triedge_api.Services;

public class AuthService(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public string GenerateToken(User user, int expiredInHours = 2)
    {
        var claims = new[]
        {
            new Claim("userid", user.Id.ToString()),
            new Claim("login", user.Login),
            new Claim("username", user.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expiredInHours),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
