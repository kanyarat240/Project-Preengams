

using Application.Common.Enums;
using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class RegisterAccount
    {
        public class Command : RegisterAccountDTO, IRequest<bool>
        {
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private ICleanDbContext _context;
            private readonly IUserAccountService _userAccountService;
            private readonly IHashingService _hashingService;
            private readonly IJWTService _JWTService;

            public Handler(ICleanDbContext context, IUserAccountService userAccountService, IHashingService hashingService, IJWTService JWTService)
            {
                _context = context;
                _userAccountService = userAccountService;
                _hashingService = hashingService;
                _JWTService = JWTService;

            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {

                int AccountId = await this.saveAccount(request, cancellationToken);
                int ProfileId = await this.saveProfile(request, AccountId);
                await this.AuthByEmail(request, cancellationToken);

                return true;

            }

            private async Task<int> saveAccount(Command request, CancellationToken cancellationToken)
            {

                if (string.IsNullOrWhiteSpace(request.Username)) throw new AppException(ErrorMessage.Authentication.EmailIsRequired);
                if (string.IsNullOrWhiteSpace(request.Password)) throw new AppException(ErrorMessage.Authentication.PasswordIsRequired);
                if (string.IsNullOrWhiteSpace(request.ConfirmPassword)) throw new AppException(ErrorMessage.Authentication.RePasswordIsRequired);
                if (request.Password != request.ConfirmPassword) throw new AppException(ErrorMessage.Authentication.PasswordNotMatch);
                //if (!validateEmail(request.Username)) throw new AppException(ErrorMessage.Authentication.EmailIncorrectFormat);
                if (request.Password.Length > 50) throw new AppException(ErrorMessage.Authentication.PasswordIncorrectFormat);

                if (await _context.UserAccount.AnyAsync(c => c.Username == request.Username))
                {
                    throw new ApplicationException("UsernameIsDuplicate.");
                }
                byte[] passwordSalt;

                DateTime today = DateTime.Now;

                UserAccount account = new UserAccount();
                account.Username = request.Username;
                account.Password = _hashingService.HashPasword(request.Password, out passwordSalt);
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

            public async Task<AccessTokenDTO?> AuthByEmail(Command request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.Username)) throw new AppException(ErrorMessage.Authentication.EmailIsRequired);
                if (string.IsNullOrWhiteSpace(request.Password)) throw new AppException(ErrorMessage.Authentication.PasswordIsRequired);

                UserAccount? user = await _context.UserAccount.FirstOrDefaultAsync(x => x.Username == request.Username);
                if (user == null)
                    throw new AppException(ErrorMessage.Authentication.UserDoesNotExist);

                if (!user.Active) throw new AppException(ErrorMessage.Authentication.AccessDenied);


                if (!_hashingService.VerifyPassword(request.Password, user.Password, Convert.FromBase64String(user.PasswordSalt)))
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

            public async Task<int> saveProfile(Command request, int userAccountId)
            {
                try
                {

                    Profiles profile = new Profiles();

                    profile.FirstName = request.FirstName;
                    profile.LastName = request.LastName;
                    profile.Phone = request.Phone;
                    profile.Address = request.Address;
                    profile.IdCard = request.IdCard;
                    profile.AccountId = userAccountId;
                    profile.RoomId = request.RoomId;
                    profile.Active = true;

                    _context.Entry(profile).State = EntityState.Added;

                    await _context.SaveChangesAsync();
                    return profile.Id;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
           
           
            //private bool validateEmail(string email)
            //{
            //    try
            //    {
            //        var teamNameWithoutSpaces = request.Replace(" ", "").ToLower();

            //        // ตรวจสอบว่ามีชื่อทีมซ้ำหรือไม่โดยไม่สนใจการเว้นบรรทัดและตัวพิมเล็กพิมใหญ่
            //        var isTeamNameExists = await _context.Teams.AnyAsync(t => t.TeamName.Replace(" ", "").ToLower() == teamNameWithoutSpaces);

            //        if (isTeamNameExists)
            //        {
            //            throw new ArgumentException("Team name already exists");
            //        }
            //    }
            //    catch
            //    {
            //        return false;
            //    }

            //}

        }

    }
}
