using Domain.Entities.DB;
using System;
namespace Domain.DTOs;

public class UserProfileDTO
{
	public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? PictureUrl { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Phone { get; set; }
    public string? Line { get; set; }
    public string? AddressDescription { get; set; }
    public string? IdCard { get; set; }
    public int AccountId { get; set; }
    public int RoomId { get; set; }
    public string? StatusMessage { get; set; }

    //public ICollection<ApplicantsCompany> ApplicantsCompany { get; set; }
}
