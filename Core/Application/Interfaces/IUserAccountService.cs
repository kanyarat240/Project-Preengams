using System;
using Domain.DTOs;

namespace Application.Interfaces;

public interface IUserAccountService
{
    Task<int> RegisterAccount(UserAccountDTO request, CancellationToken cancellationToken);
    Task<AccessTokenDTO?> AuthByEmail(string username, string password, CancellationToken cancellationToken);
}

