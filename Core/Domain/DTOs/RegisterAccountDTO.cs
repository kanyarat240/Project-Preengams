using System;
using System.Reflection.Metadata;

namespace Domain.DTOs;

public class RegisterAccountDTO
{
    //Page1
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set;}

    //Page2
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdCard { get; set; }
    public string Phone { get; set; }
    public string? Address { get; set; }
    public int RoomId { get; set; }
    public bool Active { get; set; }




}

