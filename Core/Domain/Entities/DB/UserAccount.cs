using System;
namespace Domain.Entities.DB;

public class UserAccount : EntityBase
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string? PasswordSalt { get; set; }
    public DateTime? PasswordExpiryInDateTime { get; set; }
    public DateTime? PasswordAllowChangeDateTime { get; set; }
    public DateTime? PasswordLastChangeDateTime { get; set; }
    public bool PasswordIsForceChange { get; set; }
    public int FailTimeCount { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshExpiryInDateTime { get; set; }
    public bool Lock { get; set; }
    public bool Active { get; set; }
    public ICollection<Profiles> Profiles { get; set; }
}
