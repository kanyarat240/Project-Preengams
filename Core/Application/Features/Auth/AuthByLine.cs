using Application.Interfaces;
using Domain.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.DB;

namespace Application.Features.Auth;

public class AuthByLine
{
    public record Command(string code, string state,string returnUrl) : IRequest<AccessTokenDTO?>;

    public class Handler : IRequestHandler<Command, AccessTokenDTO?>
    {
        private readonly ICleanDbContext _context;
        private readonly ILineAuthService _lineAuthService;
        private readonly IJWTService _JWTService;
        private readonly IMemoryCache _memoryCache;
        public Handler(ILineAuthService lineAuthService, IMemoryCache memoryCache, ICleanDbContext context, IJWTService jWTService)
        {
            _lineAuthService = lineAuthService;
            _JWTService = jWTService;
            _memoryCache = memoryCache;
            _context = context;
        }

        public async Task<AccessTokenDTO?> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var cache = _memoryCache.Get(request.state);
            //    if (cache is null)
            //        throw new Exception("Saction Expired.");

            //    string code_verifier = cache.ToString() ?? "";

            //    AccessTokenLineDTO? lineToken = await _lineAuthService.IssueAccessToken(request.code, code_verifier, request.returnUrl);
            //    if (lineToken == null || string.IsNullOrWhiteSpace(lineToken.AccessToken))
            //        throw new Exception("Unauthorized");

            //    LineProfileDTO? lineProfile = await _lineAuthService.GetUserProfile(lineToken.AccessToken);
            //    if (lineProfile == null || string.IsNullOrWhiteSpace(lineProfile.UserId))
            //        throw new Exception("Unauthorized");

            //    UserAccount? user = await _context.UserAccounts.FirstOrDefaultAsync(x => x.LineId == lineProfile.UserId);
            //    if (user == null)
            //    {
            //        IDTokenPayloadDTO? tokenPayload = await _lineAuthService.VerifyIDToken(lineToken.IdToken);
            //        if (tokenPayload == null)
            //            throw new Exception("Unauthorized");

            //        user = new UserAccount();
            //        user.LineId = lineProfile.UserId;
            //        user.Email = tokenPayload.email;
            //        user.Lock = false;
            //        user.Active = true;
            //        this._context.Entry(user).State = EntityState.Added;
            //    }
            //    else
            //    {
            //        this._context.Entry(user).State = EntityState.Modified;
            //    }

            //    if (!user.Active) throw new Exception("Unauthorized");

            //    user.RefreshToken = _JWTService.GenerateRefreshToken();
            //    user.RefreshExpiryInDateTime = DateTime.Now.AddDays(7);

            //    await this._context.SaveChangesAsync(cancellationToken);

            //    List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

            //    DateTime tokenExpired = DateTime.Now.AddHours(12);

            //    JwtSecurityToken jwtToken = _JWTService.CreateToken(claims, tokenExpired);

            //    return new AccessTokenDTO()
            //    {
            //        AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            //        RefreshToken = user.RefreshToken,
            //        ExpiresIn = tokenExpired,
            //        RefreshExpiryIn = user.RefreshExpiryInDateTime
            //    };
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}