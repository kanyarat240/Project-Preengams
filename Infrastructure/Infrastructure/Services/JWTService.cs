using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Domain.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JWTService : IJWTService
{
    private JWTConfig _jwtSetting;
    public JWTService(IOptions<JWTConfig> jwtSetting)
    {
        _jwtSetting = jwtSetting.Value;
    }


    public JwtSecurityToken CreateToken(List<Claim> authClaims, DateTime? tokenValidityIn = null)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
        int tokenValidityInMinutes = _jwtSetting.TokenValidityInMinutes;

        var token = new JwtSecurityToken(
            issuer: _jwtSetting.ValidIssuer,
            audience: _jwtSetting.ValidAudience,
            expires: tokenValidityIn.HasValue ? tokenValidityIn.Value : DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public List<Claim>? GetClaimFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = _jwtSetting.ValidAudience,
            ValidIssuer = _jwtSetting.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return null;

        var jwtToken = (JwtSecurityToken)securityToken;

        return jwtToken.Claims.ToList();

    }
}