using Application.Features.Auth;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    [HttpGet("line/url/auth-request")]
    public Task<string> GetAuthRequestLineURL([FromQuery] string returnUrl) => Mediator.Send(new GetLineLoginURL.Query(returnUrl));

    [HttpGet("line")]
    public Task<AccessTokenDTO?> LineAuth(string code, string state, string returnUrl) => Mediator.Send(new AuthByLine.Command(code, state, returnUrl));

    [HttpPost("login/email")]
    [AllowAnonymous]
    public Task<AccessTokenDTO?> EmailAuth([FromBody] AuthByEmail.Command body) => Mediator.Send(body);

    [HttpGet("refresh-token")]
    public Task<AccessTokenDTO> RefreshToken(string refreshToken) => Mediator.Send(new RefreshToken.Command(refreshToken));
}