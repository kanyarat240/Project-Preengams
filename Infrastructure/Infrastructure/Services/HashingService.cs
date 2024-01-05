using System;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class HashingService: IHashingService
{

    const int keySize = 32;
    const int iterations = 350000;
    HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public string HashPasword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);
        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
    }

}

