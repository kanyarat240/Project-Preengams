

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
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class UpdatePassword
    {
        public class Command : IRequest<bool>
        {
            public int Id { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }


        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private ICleanDbContext _context;
            private readonly IHashingService _hashingService;
            private readonly IUserAccountService _userAccountService;

            public Handler(ICleanDbContext context, IUserAccountService userAccountService, IHashingService hashingService)
            {
                _context = context;
                _userAccountService = userAccountService;
                _hashingService = hashingService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {

                    TimeZoneInfo gmt7TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                    if (request == null)
                    {
                        throw new ArgumentNullException("Data is null");
                    }
                    else
                    {
                        var Updatpass = _context.UserAccount.Where(t => t.Id == request.Id).FirstOrDefault();
                        if (Updatpass == null)
                        {
                            throw new ArgumentException("Data is null");
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(request.Password)) throw new AppException(ErrorMessage.Authentication.PasswordIsRequired);
                            if (string.IsNullOrWhiteSpace(request.ConfirmPassword)) throw new AppException(ErrorMessage.Authentication.RePasswordIsRequired);
                            if (request.Password != request.ConfirmPassword) throw new AppException(ErrorMessage.Authentication.PasswordNotMatch);
                            //if (!validateEmail(request.Username)) throw new AppException(ErrorMessage.Authentication.EmailIncorrectFormat);
                            if (request.Password.Length > 50) throw new AppException(ErrorMessage.Authentication.PasswordIncorrectFormat);

                          
                            byte[] passwordSalt;

                            DateTime today = DateTime.Now;

                            Updatpass.Password = _hashingService.HashPasword(request.Password, out passwordSalt);
                            Updatpass.PasswordSalt = Convert.ToBase64String(passwordSalt);
                            Updatpass.PasswordAllowChangeDateTime = today.AddDays(1);
                            Updatpass.PasswordExpiryInDateTime = today.AddDays(30);
                            Updatpass.PasswordLastChangeDateTime = null;
                            Updatpass.PasswordIsForceChange = false;



                            _context.Entry(Updatpass).State = EntityState.Modified;
                        }

                        await _context.SaveChangesAsync(cancellationToken);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    // Log the exception
                    throw new ApplicationException("An error occurred while updating the team.", e);
                }

            }

        }

    }
}
