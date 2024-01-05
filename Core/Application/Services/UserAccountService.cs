using System;
using System.Buffers.Text;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading;
using Application.Common.Enums;
using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services;

public class UserAccountService : IUserAccountService
{
    private readonly ICleanDbContext _context;
    private readonly IHashingService _hashingService;
    private readonly IJWTService _JWTService;
    private readonly AppSettingDTO _appSetting;
    public UserAccountService(ICleanDbContext context, IHashingService hashingService,IOptions<AppSettingDTO> options, IJWTService JWTService)
    {
        _context = context;
        _hashingService = hashingService;
        _appSetting = options.Value;
        _JWTService = JWTService;
    }

    public async Task<int> RegisterAccount(UserAccountDTO request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username)) throw new AppException(ErrorMessage.Authentication.EmailIsRequired);
        if (string.IsNullOrWhiteSpace(request.Password)) throw new AppException(ErrorMessage.Authentication.PasswordIsRequired);
        if (string.IsNullOrWhiteSpace(request.RePassword)) throw new AppException(ErrorMessage.Authentication.RePasswordIsRequired);
        if (request.Password != request.RePassword) throw new AppException(ErrorMessage.Authentication.PasswordNotMatch);
        if (!validateEmail(request.Username)) throw new AppException(ErrorMessage.Authentication.EmailIncorrectFormat);
        if (request.Password.Length > 50) throw new AppException(ErrorMessage.Authentication.PasswordIncorrectFormat);

        if (_context.UserAccount.Any(c => c.Username == request.Username)) throw new AppException(ErrorMessage.Authentication.EmailIsDuplicate);

        byte[] passwordSalt;

        DateTime today = DateTime.Now;

        UserAccount account = new UserAccount();
        account.Username = request.Username;
        account.Password = _hashingService.HashPasword(request.Password,out passwordSalt);
        account.PasswordSalt = Convert.ToBase64String(passwordSalt);
        account.PasswordAllowChangeDateTime = today.AddDays(1);
        account.PasswordExpiryInDateTime = today.AddDays(30);
        account.PasswordLastChangeDateTime = null;
        account.PasswordIsForceChange = false;
        account.Active = true;
        _context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        await _context.SaveChangesAsync(cancellationToken);
        return account.Id;
    }

    public async Task<AccessTokenDTO?> AuthByEmail(string Username, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new AppException(ErrorMessage.Authentication.EmailIsRequired);
        if (string.IsNullOrWhiteSpace(password)) throw new AppException(ErrorMessage.Authentication.PasswordIsRequired);

        UserAccount? user = await _context.UserAccount.FirstOrDefaultAsync(x => x.Username == Username);
        if (user == null)
            throw new AppException(ErrorMessage.Authentication.UserDoesNotExist);

        if (!user.Active) throw new AppException(ErrorMessage.Authentication.AccessDenied);


        if (!_hashingService.VerifyPassword(password, user.Password, Convert.FromBase64String(user.PasswordSalt)))
            throw new AppException(ErrorMessage.Authentication.PasswordIncorrect);

        user.RefreshToken = _JWTService.GenerateRefreshToken();
        user.RefreshExpiryInDateTime = DateTime.Now.AddDays(7);
        this._context.Entry(user).State = EntityState.Modified;
        await this._context.SaveChangesAsync(cancellationToken);

        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        DateTime tokenExpired = DateTime.Now.AddHours(12);

        JwtSecurityToken jwtToken = _JWTService.CreateToken(claims, tokenExpired);

        return new AccessTokenDTO()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            RefreshToken = user.RefreshToken,
            ExpiresIn = tokenExpired,
            RefreshExpiryIn = user.RefreshExpiryInDateTime
        };
    }

    private bool validateEmail(string email)
    {
        try
        {
            var emailAddress = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }

    }
}

