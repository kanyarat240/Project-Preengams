using System;
using Application.Interfaces;
using Application.Services;
using Domain.DTOs;
using MediatR;

namespace Application.Features.User;

public class RegisterTrainee
{
    public class Command : UserAccountDTO, IRequest<bool>
    {
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private ICleanDbContext _context;
        private readonly IUserAccountService _userAccountService;

        public Handler(ICleanDbContext context, IUserAccountService userAccountService)
        {
            _context = context;
            _userAccountService = userAccountService;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            await _userAccountService.RegisterAccount(request, cancellationToken);
            return true;
        }
    }
}

