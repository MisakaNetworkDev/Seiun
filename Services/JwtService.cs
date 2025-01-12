using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Seiun.Entities;
using Seiun.Utils;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Seiun.Services;

public class JwtService(IConfiguration configuration) : IJwtService 
{
    private readonly string _secret =
        configuration["Jwt:Secret"] ?? throw new ArgumentException(configuration["Jwt:Secret"]);

    private readonly string _issuer =
        configuration["Jwt:Issuer"] ?? throw new ArgumentException(configuration["Jwt:Issuer"]);

    private readonly string _audience =
        configuration["Jwt:Audience"] ?? throw new ArgumentException(configuration["Jwt:Audience"]);

    public string GenerateToken(UserEntity userEntity)
    {
        // token payload
        Claim[] claims =
        [
            new Claim(ClaimTypes.Name, userEntity.UserName),
            new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),
            new Claim(ClaimTypes.Role, userEntity.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        // set token expiration time
        var expiration = DateTime.UtcNow.AddHours(Constants.Token.TokenExpirationTime);

        // sign token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, Constants.Token.SignAlgorithm);

        // create token
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}