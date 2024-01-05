using System;
using Application.Interfaces;
using System.Security.Cryptography;
using Domain.Entities.DB;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Features.Auth;

public class RefreshToken
{
    public record Command(string refreshToken) : IRequest<AccessTokenDTO>;

    public class Handler : IRequestHandler<Command, AccessTokenDTO>
    {
        private readonly ICleanDbContext _context;
        private readonly IJWTService _JWTService;
        public Handler(ICleanDbContext context,IJWTService JWTService)
        {
            _context = context;
            _JWTService = JWTService;
        }

        public async Task<AccessTokenDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.refreshToken)) throw new Exception("Unauthorized");
            UserAccount? user = await _context.UserAccount.FirstOrDefaultAsync(x => x.RefreshToken == request.refreshToken);

            if(user is null)
                throw new Exception("Unauthorized");

            if(user.RefreshExpiryInDateTime.Value < DateTime.Now)
            {
                user.RefreshToken = null;
                user.RefreshExpiryInDateTime = null;
                this._context.Entry(user).State = EntityState.Modified;
                await this._context.SaveChangesAsync(cancellationToken);
                throw new Exception("Unauthorized");
            }
            else
            {
                user.RefreshToken = _JWTService.GenerateRefreshToken();
                user.RefreshExpiryInDateTime = DateTime.Now.AddDays(7);
                this._context.Entry(user).State = EntityState.Modified;
                await this._context.SaveChangesAsync(cancellationToken);

                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

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
        }
    }
}

