
using Application.Interfaces;
using Domain.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.DB;
using Application.Common.Enums;

namespace Application.Features.Auth;

public class AuthByEmail
{
    public record Command(string username, string password) : IRequest<AccessTokenDTO?>;

    public class Handler : IRequestHandler<Command, AccessTokenDTO?>
    {
        private readonly IUserAccountService _userAccountService;
        public Handler(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public Task<AccessTokenDTO?> Handle(Command request, CancellationToken cancellationToken)
        {
            return _userAccountService.AuthByEmail(request.username, request.password, cancellationToken);
        }
    }
}