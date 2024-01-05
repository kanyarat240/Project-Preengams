using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Interfaces;

public interface IJWTService
{
    JwtSecurityToken CreateToken(List<Claim> authClaims, DateTime? tokenValidityIn = null);
    string GenerateRefreshToken();
}

