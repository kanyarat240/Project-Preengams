using System;
using Application.Interfaces;
using Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class ValidateProfileComplete
{
    public record Query() : IRequest<bool>;

    public class Handler : IRequestHandler<Query, bool>
    {
        private ICleanDbContext _context;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        public Handler(ICleanDbContext context, ICurrentUserAccessor currentUserAccessor)
        {
            _context = context;
            _currentUserAccessor = currentUserAccessor;
        }

        public Task<bool> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
            //return _context.Employees.AnyAsync(x=>x.UserId == _currentUserAccessor.UserId);
        }
    }
}

