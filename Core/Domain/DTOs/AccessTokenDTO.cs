namespace Domain.DTOs;

public class AccessTokenDTO
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresIn { get; set; }
    public DateTime? RefreshExpiryIn { get; set; }
}