using System;
namespace Domain.Configs;

public class JWTConfig
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
    public int TokenValidityInMinutes { get; set; }
    public int RefreshTokenValidityInDays { get; set; }

}

