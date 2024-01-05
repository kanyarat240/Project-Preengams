using System;
namespace Application.Interfaces;

public interface IHashingService
{

    public string HashPasword(string password, out byte[] salt);

    public bool VerifyPassword(string password, string hash, byte[] salt);

}

